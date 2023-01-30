using EntityFrameworkClassLibrary;
using EntityFrameworkClassLibrary.Repository;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiFunctionWithRepositoryPattern
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder service)
        {
            //service.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            //service.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            service.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EsercizioAPI;" +
                "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            service.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
