using EntityFrameworkClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            var invoice = await _appDbContext.Invoices
                .Include(y => y.Customer)
                .ToListAsync();

            return invoice;
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _appDbContext.Invoices.FindAsync(id);
        }

        public async Task AddInvoice(Invoice invoice)
        {
            await _appDbContext.Invoices.AddAsync(invoice);
        }

        public async Task<string> RemoveInvoiceById(int id)
        {
            var dbInvoice = await _appDbContext.Invoices.FindAsync(id);

            if (dbInvoice == null)
                return "Id Not Found!";

            _appDbContext.Remove(dbInvoice);
            return "Invoice Removed!";
        }

        public async Task<string> UpdateInvoice(Invoice invoice)
        {
            Invoice? dbInvoice = await _appDbContext.Invoices.FindAsync(invoice.Id);

            if (dbInvoice == null)
                return "Id Not Found";

            dbInvoice.OrderDate = invoice.OrderDate;
            dbInvoice.Quantity = invoice.Quantity;
            dbInvoice.Price = invoice.Price;
            dbInvoice.CustomerId = invoice.CustomerId;
            _appDbContext.Invoices.Update(dbInvoice);
            return "Invoice Updated";
        }
    }
}
