using EntityFrameworkClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

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
                .Include(x => x.Product)
                .ToListAsync();

            return invoice;
        }

        public async Task<Invoice?> GetInvoiceById(int id)
        {
            Invoice? invoice = await _appDbContext.Invoices
                .Include(y => y.Customer)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(invoice == null)
                return null;

            return invoice;
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
