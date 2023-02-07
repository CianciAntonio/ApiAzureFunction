using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EntityFrameworkClassLibrary.Models;
using EntityFrameworkClassLibrary.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

//Attributo in cui dico alla funzione di caricare la classe Startup all'avvio del programma
[assembly: FunctionsStartup(typeof(ApiFunctionWithRepositoryPattern.Startup))]

namespace ApiFunctionWithRepositoryPattern
{ 
    public class ApiFunctionWithRepositoryPattern
    {
        private readonly IService _service;

        public ApiFunctionWithRepositoryPattern(IService service)
        {
            _service = service;
        }

        [FunctionName("GetAllCustomers")]
        public async Task<IActionResult> GetCustomers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var customers = await _service.GetAllCustomers();
            return new OkObjectResult(customers);
        }

        [FunctionName("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetCustomerById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var customer = await _service.GetCustomerById(id);
            return new OkObjectResult(customer);
        }

        [FunctionName("AddCustomer")]
        public async Task<IActionResult> AddCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Customer newCustomer = JsonConvert.DeserializeObject<Customer>(requestBody);

            await _service.AddCustomer(newCustomer);

            return new OkObjectResult("Customer Added!");
        }

        [FunctionName("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Customer newCustomer = JsonConvert.DeserializeObject<Customer>(requestBody);

            var result = await _service.UpdateCustomer(newCustomer);

            return new OkObjectResult(result);
        }

        [FunctionName("DeleteCustomerById")]
        public async Task<IActionResult> DeleteCustomerById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteCustomerById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            await _service.RemoveCustomer(id);

            return new OkObjectResult("Customer Removed");
        }

        [FunctionName("GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoices(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var invoices = await _service.GetAllInvoices();

            return new OkObjectResult(invoices);
        }

        [FunctionName("GetInvoiceById")]
        public async Task<IActionResult> GetInvoiceById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetInvoiceById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var invoice = await _service.GetInvoiceById(id);

            return new OkObjectResult(invoice);
        }

        [FunctionName("AddInvoice")]
        public async Task<IActionResult> AddInvoice(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic newInvoice = JsonConvert.DeserializeObject<Invoice>(requestBody);

            await _service.AddInvoice(newInvoice);

            return new OkObjectResult("Invoice added!");
        }

        [FunctionName("UpdateInvoice")]
        public async Task<IActionResult> UpdateInvoice(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic newInvoice = JsonConvert.DeserializeObject<Invoice>(requestBody);

            string message = await _service.UpdateInvoice(newInvoice);

            return new OkObjectResult(message);
        }

        [FunctionName("DeleteInvoiceById")]
        public async Task<IActionResult> DeleteInvoiceById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteInvoiceById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            await _service.RemoveInvoice(id);

            return new OkObjectResult("Invoice Removed");
        }

        [FunctionName("AddProduct")]
        public async Task<IActionResult> AddProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic newProduct = JsonConvert.DeserializeObject<Product>(requestBody);

            await _service.AddProduct(newProduct);

            return new OkObjectResult("Product added!");
        }

        [FunctionName("DeleteProductById")]
        public async Task<IActionResult> DeleteProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteProductById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            await _service.RemoveProduct(id);

            return new OkObjectResult("Product Removed");
        }

        #region DEPENDENCY INJECTION (NO UoW)
        //private ICustomerRepository _customerRepo;
        //private IInvoiceRepository _invoiceRepo;

        //public ApiFunctionWithRepositoryPattern(ICustomerRepository customerRepo, IInvoiceRepository invoiceRepo)
        //{
        //    _customerRepo = customerRepo;
        //    _invoiceRepo = invoiceRepo;
        //}
        #endregion
        #region CONTROLLER (NO UoW)
        //#region GET ALL CUSTOMERS
        //[FunctionName("GetAllCustomers")]
        //public async Task<IActionResult> GetAllCustomers(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var customers = _customerRepo.GetAllCustomers();

        //    return new OkObjectResult(customers);
        //}
        //#endregion

        //#region GET CUSTOMER BY ID
        //[FunctionName("GetCustomerById")]
        //public async Task<IActionResult> GetCustomerById(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetCustomerById/{id}")] HttpRequest req,
        //    ILogger log, int id)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var customer = await _customerRepo.GetCustomerById(id);

        //    return new OkObjectResult(customer);
        //}
        //#endregion

        //#region ADD CUSTOMER
        //[FunctionName("AddCustomer")]
        //public async Task<IActionResult> AddCustomer(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic newCustomer = JsonConvert.DeserializeObject<Customer>(requestBody);

        //    await _customerRepo.AddCustomer(newCustomer);

        //    return new OkObjectResult("Customer added!");
        //}
        //#endregion

        //#region UPDATE CUSTOMER
        //[FunctionName("UpdateCustomer")]
        //public async Task<IActionResult> UpdateCustomer(
        //    [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic newCustomer = JsonConvert.DeserializeObject<Customer>(requestBody);

        //    await _customerRepo.UpdateCustomer(newCustomer);

        //    return new OkObjectResult("Customer updated!");
        //}
        //#endregion

        //#region DELETE CUSTOMER BY ID
        //[FunctionName("DeleteCustomerById")]
        //public async Task<IActionResult> DeleteCustomerById(
        //    [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteCustomerById/{id}")] HttpRequest req,
        //    ILogger log, int id)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    await _customerRepo.RemoveCostomerById(id);

        //    return new OkObjectResult("Customer removed!");
        //}
        //#endregion

        //#region GET ALL INVOICES
        //[FunctionName("GetAllInvoices")]
        //public async Task<IActionResult> GetAllInvoices(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var invoices = _invoiceRepo.GetAllInvoices();

        //    return new OkObjectResult(invoices);
        //}
        //#endregion

        //#region GET INVOICE BY ID
        //[FunctionName("GetInvoiceById")]
        //public async Task<IActionResult> GetInvoiceById(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetInvoiceById/{id}")] HttpRequest req,
        //    ILogger log, int id)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var invoice = await _invoiceRepo.GetInvoiceById(id);

        //    return new OkObjectResult(invoice);
        //}
        //#endregion

        //#region ADD INVOICE
        //[FunctionName("AddInvoice")]
        //public async Task<IActionResult> AddInvoice(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic newProduct = JsonConvert.DeserializeObject<Invoice>(requestBody);

        //    await _invoiceRepo.AddInvoice(newProduct);

        //    return new OkObjectResult("Invoice added!");
        //}
        //#endregion

        //#region UPDATE INVOICE
        //[FunctionName("UpdateInvoice")]
        //public async Task<IActionResult> UpdateInvoice(
        //    [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic newProduct = JsonConvert.DeserializeObject<Invoice>(requestBody);

        //    await _invoiceRepo.UpdateInvoice(newProduct);

        //    return new OkObjectResult("Invoice updated!");
        //}
        //#endregion

        //#region DELETE INVOICE BY ID
        //[FunctionName("DeleteInvoiceById")]
        //public async Task<IActionResult> DeleteInvoiceById(
        //    [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteInvoiceById/{id}")] HttpRequest req,
        //    ILogger log, int id)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    await _invoiceRepo.RemoveInvoiceById(id);

        //    return new OkObjectResult("Invoice removed!");
        //}
        //#endregion
        #endregion
    }
}