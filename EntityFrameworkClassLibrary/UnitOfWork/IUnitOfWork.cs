using EntityFrameworkClassLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkClassLibrary.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository customer { get; }
        IInvoiceRepository invoice { get; }
        IProductRepository product { get; }
        Task Save();
        void Dispose();
    }
}
