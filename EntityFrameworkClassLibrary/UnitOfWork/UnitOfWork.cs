using EntityFrameworkClassLibrary.Repository;
using EntityFrameworkClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkClassLibrary.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IInvoiceRepository invoice { get; set; }
        public ICustomerRepository customer { get; set; }
        public IProductRepository product { get; set; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            invoice = new InvoiceRepository(_appDbContext);
            customer = new CustomerRepository(_appDbContext);
            product = new ProductRepository(_appDbContext);
        }

        public Task Save()
        {
            return _appDbContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _appDbContext.DisposeAsync();
        }
    }
}
