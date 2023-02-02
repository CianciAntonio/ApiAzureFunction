using EntityFrameworkClassLibrary.Models;

namespace EntityFrameworkClassLibrary.Repository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetAllInvoices();
        Task<Invoice?> GetInvoiceById(int id);
        Task AddInvoice(Invoice invoice);
        Task<string> RemoveInvoiceById(int id);
        Task<string> UpdateInvoice(Invoice invoice);
    }
}
