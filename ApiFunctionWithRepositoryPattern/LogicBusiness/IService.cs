using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFrameworkClassLibrary.Models;
using Dtos.CustomerResponse;
using Dtos.ModelRequest;

namespace ApiFunctionWithRepositoryPattern.LogicBusiness
{
    public interface IService
    {
        Task<List<CustomerResponse>> GetAllCustomers();
        Task<CustomerResponse> GetCustomerById(int id);
        Task AddCustomer(CustomerRequest customerReq);
        Task<CustomerResponse> UpdateCustomer(CustomerRequest customerReq,int id);
        Task<CustomerResponse> RemoveCustomer(int id);
        Task<List<InvoiceResponse>> GetAllInvoices();
        Task<InvoiceResponse> GetInvoiceById(int id);
        Task AddInvoice(InvoiceRequest invoiceReq);
        Task<InvoiceResponse> UpdateInvoice(InvoiceRequest invoiceReq,int id);
        Task<InvoiceResponse> RemoveInvoice(int id);
        Task AddProduct(ProductRequest productReq);
        Task<ProductResponse> RemoveProduct(int id);
    }
}
