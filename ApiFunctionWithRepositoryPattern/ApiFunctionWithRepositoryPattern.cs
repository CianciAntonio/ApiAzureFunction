using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using ApiFunctionWithRepositoryPattern.LogicBusiness;
using Dtos.CustomerResponse;
using Dtos.ModelRequest;
using System.Collections.Generic;

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

        [FunctionName("AddCustomer")]
        [OpenApiOperation(operationId: "AddCustomer", tags: new[] { "Customer" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerRequest))]
        [OpenApiResponseWithoutBody(HttpStatusCode.Created)]
        public async Task<IActionResult> AddCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CustomerRequest requestCustomer = JsonConvert.DeserializeObject<CustomerRequest>(requestBody);

            await _service.AddCustomer(requestCustomer);

            return new OkObjectResult(requestCustomer);
        }        

        [FunctionName("GetAllCustomers")]
        [OpenApiOperation(operationId: "GetAllCustomers", tags: new[] { "Customer" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<CustomerResponse> result = await _service.GetAllCustomers();

            return new OkObjectResult(result);
        }
        
        [FunctionName("GetCustomerById")]
        [OpenApiOperation(operationId: "GetCustomerById", tags: new[] { "Customer" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Customer Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Found)]
        public async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetCustomerById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            CustomerResponse customer = await _service.GetCustomerById(id);

            return new OkObjectResult(customer);
        }        

        [FunctionName("UpdateCustomer")]
        [OpenApiOperation(operationId: "UpdateCustomer", tags: new[] { "Customer" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Customer Id")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerRequest))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateCustomer/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CustomerRequest newCustomer = JsonConvert.DeserializeObject<CustomerRequest>(requestBody);

            CustomerResponse response = await _service.UpdateCustomer(newCustomer, id);

            return new OkObjectResult(response);
        }
        
        [FunctionName("DeleteCustomerById")]
        [OpenApiOperation(operationId: "DeleteCustomerById", tags: new[] { "Customer" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Customer Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomerById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteCustomerById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            CustomerResponse response = await _service.RemoveCustomer(id);

            return new OkObjectResult(response);
        }

        
        [FunctionName("AddInvoice")]
        [OpenApiOperation(operationId: "AddInvoice", tags: new[] { "Invoice" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceRequest))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Created)]
        public async Task<IActionResult> AddInvoice(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            InvoiceRequest invoiceRequest = JsonConvert.DeserializeObject<InvoiceRequest>(requestBody);

            await _service.AddInvoice(invoiceRequest);

            return new OkObjectResult(invoiceRequest);
        }

        [FunctionName("GetAllInvoices")]
        [OpenApiOperation(operationId: "GetAllInvoices", tags: new[] { "Invoice" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInvoices(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<InvoiceResponse> invoices = await _service.GetAllInvoices();

            return new OkObjectResult(invoices);
        }

        [FunctionName("GetInvoiceById")]
        [OpenApiOperation(operationId: "GetInvoiceById", tags: new[] { "Invoice" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Invoice Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Found)]
        public async Task<IActionResult> GetInvoiceById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetInvoiceById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            InvoiceResponse invoice = await _service.GetInvoiceById(id);

            return new OkObjectResult(invoice);
        }

        [FunctionName("UpdateInvoice")]
        [OpenApiOperation(operationId: "UpdateInvoice", tags: new[] { "Invoice" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Invoice Id")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceRequest))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateInvoice(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateInvoice/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            InvoiceRequest newInvoice = JsonConvert.DeserializeObject<InvoiceRequest>(requestBody);

            InvoiceResponse response = await _service.UpdateInvoice(newInvoice, id);

            return new OkObjectResult(response);
        }

        [FunctionName("DeleteInvoiceById")]
        [OpenApiOperation(operationId: "DeleteInvoiceById", tags: new[] { "Invoice" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Delete Invoice")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteInvoiceById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteInvoiceById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            InvoiceResponse response = await _service.RemoveInvoice(id);

            return new OkObjectResult(response);
        }

        [FunctionName("AddProduct")]
        [OpenApiOperation(operationId: "AddProduct", tags: new[] { "Product" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ProductRequest))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Created)]
        public async Task<IActionResult> AddProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ProductRequest productRequest = JsonConvert.DeserializeObject<ProductRequest>(requestBody);
            
            await _service.AddProduct(productRequest);

            return new OkObjectResult(productRequest);
        }

        [FunctionName("DeleteProductById")]
        [OpenApiOperation(operationId: "DeleteProductById", tags: new[] { "Product" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Product Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteProductById/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ProductResponse response = await _service.RemoveProduct(id);

            return new OkObjectResult(response);
        }
    }
}