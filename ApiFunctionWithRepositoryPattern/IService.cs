using EntityFrameworkClassLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkClassLibrary.UnitOfWork;
using EntityFrameworkClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFunctionWithRepositoryPattern
{
    public interface IService
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<Customer> RemoveCustomer(int id);
        Task<IEnumerable<Invoice>> GetAllInvoices();
        Task<Invoice> GetInvoiceById(int id);
        Task AddInvoice(Invoice invoice);
        Task<Invoice> UpdateInvoice(Invoice invoice);
        Task<Invoice> RemoveInvoice(int id);
        Task AddProduct(Product product);
        Task<Product> RemoveProduct(int id);
    }
}
