using EntityFrameworkClassLibrary.Models;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiFunctionWithRepositoryPattern
{
    public class Service : IService
    {
        public IUnitOfWork _unitOfWork { get; set; }

        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllCustomers();

            if (customers == null)
                return null;

            _unitOfWork.Dispose();
            return customers;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer = await _unitOfWork.CustomerRepository.GetCustomerById(id);

            if (customer == null)
                return null;

            _unitOfWork.Dispose();
            return customer;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _unitOfWork.CustomerRepository.AddCustomer(customer);
            await _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            Customer dbCustomer = await _unitOfWork.CustomerRepository.GetCustomerById(customer.Id);

            if (dbCustomer == null)
                return null;
            
            dbCustomer.SurName = customer.SurName;
            dbCustomer.LastName = customer.LastName;
            _unitOfWork.CustomerRepository.UpdateCustomer(dbCustomer);

            await _unitOfWork.Save();
            _unitOfWork.Dispose();
            
            return dbCustomer;
        }

        public async Task<Customer> RemoveCustomer(int id)
        {
            Customer dbCustomer = await _unitOfWork.CustomerRepository.GetCustomerById(id);

            if (dbCustomer == null)
                return null;

            _unitOfWork.CustomerRepository.RemoveCustomer(dbCustomer);
            await _unitOfWork.Save();
            _unitOfWork.Dispose();

            return dbCustomer;        
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            var invoices = await _unitOfWork.InvoiceRepository.GetAllInvoices();

            if(invoices == null)
                return null;
            
            _unitOfWork.Dispose();
            return invoices;       
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetInvoiceById(id);

            if (invoice == null)
                return null;

            _unitOfWork.Dispose();
            return invoice;
        }

        public async Task AddInvoice(Invoice invoice)
        {
            await _unitOfWork.InvoiceRepository.AddInvoice(invoice);
            await _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public async Task<Invoice> UpdateInvoice(Invoice invoice)
        {
            Invoice dbInvoice = await _unitOfWork.InvoiceRepository.GetInvoiceById(invoice.Id);

            if (dbInvoice == null)
                return null;

            dbInvoice.OrderDate = invoice.OrderDate;
            dbInvoice.Quantity = invoice.Quantity;
            dbInvoice.Price = invoice.Price;
            dbInvoice.CustomerId = invoice.CustomerId;
            _unitOfWork.InvoiceRepository.UpdateInvoice(dbInvoice);

            await _unitOfWork.Save();
            _unitOfWork.Dispose();

            return invoice;
        }

        public async Task<Invoice> RemoveInvoice(int id)
        {
            var dbInvoice = await _unitOfWork.InvoiceRepository.GetInvoiceById(id);

            if(dbInvoice == null)
                return null;

            _unitOfWork.InvoiceRepository.RemoveInvoice(dbInvoice);
            await _unitOfWork.Save();
            _unitOfWork.Dispose();
            
            return dbInvoice;
        }

        public async Task AddProduct(Product product)
        {
            await _unitOfWork.ProductRepository.AddProduct(product);
            await _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public async Task<Product> RemoveProduct(int id)
        {
            Product dbProduct = await _unitOfWork.ProductRepository.GetProduct(id);

            if(dbProduct==null)
                return null;
            _unitOfWork.ProductRepository.RemoveProduct(dbProduct);
            await _unitOfWork.Save();
            _unitOfWork.Dispose();

            return dbProduct;
        }
    }
}
