using EntityFrameworkClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkClassLibrary.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private readonly AppDbContext _appDbContext;

        public InvoiceRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            var invoice = await _appDbContext.Invoices
                .AsNoTracking() // Regalo da Loris: studiare perché ottimizza le query
                .Include(y => y.Customer)
                .Include(x => x.Product)
                .ToListAsync();

            return invoice;
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            Invoice invoice = await _appDbContext.Invoices
                .AsNoTracking() // Regalo da Loris: studiare perché ottimizza le query
                .Include(y => y.Customer)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            return invoice;
        }

        public async Task AddInvoice(Invoice invoice)
        {
            await _appDbContext.Invoices.AddAsync(invoice);
        }

        public void RemoveInvoice(Invoice invoice)
        {
            _appDbContext.Invoices.Remove(invoice);
        }

        //public async Task<string> UpdateInvoice(Invoice invoice)
        //{
        //    Invoice dbInvoice = await _appDbContext.Invoices.FindAsync(invoice.Id);

        //    if (dbInvoice == null)
        //        return "Id Not Found";

        //    dbInvoice.OrderDate = invoice.OrderDate;
        //    dbInvoice.Quantity = invoice.Quantity;
        //    dbInvoice.Price = invoice.Price;
        //    dbInvoice.CustomerId = invoice.CustomerId;
        //    _appDbContext.Invoices.Update(dbInvoice);
        //    return "Invoice Updated";
        //}

        public void UpdateInvoice(Invoice invoice)
        {
            _appDbContext.Invoices.Update(invoice);
        }
    }
}
