using EntityFrameworkClassLibrary;
using EntityFrameworkClassLibrary.Repository;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApiFunctionWithRepositoryPattern
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder service)
        {
            //service.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            //service.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            service.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

            service.Services.AddDbContext<AppDbContext>( options => options.UseSqlServer(connectionString));
        }
    }
}