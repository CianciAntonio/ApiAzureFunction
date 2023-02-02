using EntityFrameworkClassLibrary.Repository;

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

        //Non è consigliabile salvare le modifiche nelle repository, meglio creare il metodo nella unitofwork e richiamarlo nella funzione
        //Questo perchè viola il principio di separazione delle responsabilità. La repository è responsabile della gestione delle entità,
        //mentre l'Unit of Work è responsabile della gestione delle transazioni e delle modifiche apportate alle entità.
        public Task Save()
        {
            return _appDbContext.SaveChangesAsync();
        }

        //Permette di liberare risorse gestite (esempio connessioni al database) che sono state allocate durante l'utilizzo del contesto.
        //Questo aiuta a prevenire eventuali perdite di memoria o problemi di prestazioni
        public async void Dispose()
        {
            await _appDbContext.DisposeAsync();
        }
    }
}
