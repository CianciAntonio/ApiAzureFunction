using EntityFrameworkClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkClassLibrary.Repository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetAllInvoices();
        Task<Invoice> GetInvoiceById(int id);
        Task AddInvoice(Invoice invoice);
        Task<string> RemoveInvoiceById(int id);
        Task<string> UpdateInvoice(Invoice invoice);
    }
}
