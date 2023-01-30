using EntityFrameworkClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkClassLibrary.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private AppDbContext _appDbContext;

        public InvoiceRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return _appDbContext.Invoices.ToList();
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _appDbContext.Invoices.FindAsync(id);
        }

        public async Task AddInvoice(Invoice invoice)
        {
            await _appDbContext.Invoices.AddAsync(invoice);
        }

        public async Task RemoveInvoiceById(int id)
        {
            var dbInvoice = await _appDbContext.Invoices.FindAsync(id);
            _appDbContext.Remove(dbInvoice);
        }

        public async Task<Invoice> UpdateInvoice(Invoice invoice)
        {
            Invoice? dbInvoice = await _appDbContext.Invoices.FindAsync(invoice.Id);

            if (dbInvoice == null)
                return null;

            dbInvoice.OrderDate = invoice.OrderDate;
            dbInvoice.Quantity = invoice.Quantity;
            dbInvoice.Price = invoice.Price;
            dbInvoice.Description = invoice.Description;
            dbInvoice.CustomerId = invoice.CustomerId;

            _appDbContext.Invoices.Update(dbInvoice);

            return dbInvoice;
        }
    }
}
