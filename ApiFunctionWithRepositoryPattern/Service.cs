using EntityFrameworkClassLibrary.Models;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(ApiFunctionWithRepositoryPattern.Startup))]

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
            IEnumerable<Customer> customers = null;
            try
            {
                customers = await _unitOfWork.CustomerRepository.GetAllCustomers();
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Null");
            }
            return customers;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer = null;
            //try
            //{
                customer = await _unitOfWork.CustomerRepository.GetCustomerById(id);
            //}
            //catch(NullReferenceException)
            //{
            //    Console.WriteLine("Null");
            //}
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
            
            dbCustomer.Name = customer.Name;
            dbCustomer.LastName = customer.LastName;
            _unitOfWork.CustomerRepository.UpdateCustomer(dbCustomer);

            await _unitOfWork.Save();
            _unitOfWork.Dispose();
            
            return dbCustomer;
        }

        public async Task RemoveCustomer(int id)
        {
            try
            {
                var dbCustomer = await _unitOfWork.CustomerRepository.GetCustomerById(id);

                _unitOfWork.CustomerRepository.RemoveCustomer(dbCustomer);
                await _unitOfWork.Save();
                _unitOfWork.Dispose();
            }
            catch (NullReferenceException ex)
            {
                throw new Exception("error", ex);
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            try
            {
                var invoices = await _unitOfWork.InvoiceRepository.GetAllInvoices();
                _unitOfWork.Dispose();
                return invoices;
            }
            catch(NullReferenceException ex)
            {
                throw new Exception("error", ex);
            }
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            try
            {
                var invoice = await _unitOfWork.InvoiceRepository.GetInvoiceById(id);
                _unitOfWork.Dispose();
                return invoice;
            }
            catch (NullReferenceException e)
            {
                throw new Exception("error", e);
            }
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
            
            try
            {
                dbInvoice.OrderDate = invoice.OrderDate;
                dbInvoice.Quantity = invoice.Quantity;
                dbInvoice.Price = invoice.Price;
                dbInvoice.CustomerId = invoice.CustomerId;

                _unitOfWork.InvoiceRepository.UpdateInvoice(dbInvoice);

                await _unitOfWork.Save();
                _unitOfWork.Dispose();
            }
            catch(NullReferenceException ex) 
            {
                throw new Exception("error", ex);
            }

            return invoice;
        }

        public async Task RemoveInvoice(int id)
        {
            try
            {
                var dbInvoice = await _unitOfWork.InvoiceRepository.GetInvoiceById(id);

                _unitOfWork.InvoiceRepository.RemoveInvoice(dbInvoice);
                await _unitOfWork.Save();
                _unitOfWork.Dispose();
            }
            catch(NullReferenceException ex)
            {
                throw new Exception("error", ex);
            }
        }

        public async Task AddProduct(Product product)
        {
            await _unitOfWork.ProductRepository.AddProduct(product);
            await _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public async Task RemoveProduct(int id)
        {
            try
            {
                await _unitOfWork.ProductRepository.RemoveProduct(id);
                await _unitOfWork.Save();
                _unitOfWork.Dispose();
            }
            catch (NullReferenceException ex)
            {
                throw new Exception("error", ex);
            }
        }
    }
}
