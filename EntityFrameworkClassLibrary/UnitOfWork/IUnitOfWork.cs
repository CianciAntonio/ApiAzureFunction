using EntityFrameworkClassLibrary.Repository;

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
