using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ApiAzureFunction.Models;
using System.Collections.Generic;
using EntityFrameworkCode;
using EntityFrameworkCode.Models;

namespace ApiAzureFunction
{
    public static class ApiAzureFunction
    {
        #region ADD NEW CUSTOMER
        [FunctionName("AddNewCustomer")]
        public static async Task<IActionResult> AddNewCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic newCustomer = JsonConvert.DeserializeObject<Customer>(requestBody);

            using (AppDbContext context = new AppDbContext())
            {
                context.Customers.Add(newCustomer);

                context.SaveChanges();
            }

            return new OkObjectResult("Customer Added!");
        }
        #endregion

        #region UPDATE CUSTOMER
        [FunctionName("UpdateCustomerById")]
        public static async Task<IActionResult> UpdateCustomerById(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "UpdateCustomerById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic updateCustomer = JsonConvert.DeserializeObject<Customer>(requestBody);

            using (AppDbContext context = new AppDbContext())
            {
                var customer = context.Customers.Find(id);

                if(customer != null)
                {
                    customer.Name = updateCustomer.Nome;
                    customer.LastName = updateCustomer.Cognome;
                }
                else
                {
                    return new OkObjectResult("Selected ID doesn't exist!");
                }

                context.SaveChanges();
            }

            return new OkObjectResult("Customer updated!");
        }
        #endregion

        #region GET ALL CUSTOMERS
        [FunctionName("GetAllCustomers")]
        public static async Task<IActionResult> GetAllCustomers(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<Customer> customerList = new List<Customer>();

            using (AppDbContext context = new AppDbContext())
            {
                var allCustomers = context.Customers;

                foreach (var cliente in allCustomers)
                {
                    customerList.Add(cliente);
                }

                return new OkObjectResult(customerList);
            }
        }
        #endregion

        #region GET CUSTOMER BY ID
        [FunctionName("GetCustomerById")]
        public static async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetCustomerById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            using (AppDbContext context = new AppDbContext())
            {
                var customer = context.Customers.Find(id);  

                return new OkObjectResult(customer);
            }
        }
        #endregion

        #region DELETE CUSTOMER
        [FunctionName("DeleteCustomerById")]
        public static async Task<IActionResult> DeleteCustomerById(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteCustomerById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            using (AppDbContext context = new AppDbContext())
            {
                var customer = context.Customers.Find(id);
                context.Customers.Remove(customer);

                context.SaveChanges();

                return new OkObjectResult("Customer Removed!");
            }
        }
        #endregion
    }
}