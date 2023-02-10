using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EntityFrameworkClassLibrary;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ApiFunctionWithRepositoryPattern
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder service)
        {
            //Evita l'errore di loop
            service.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //Dependency Injection basata su Scoped
            service.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            service.Services.AddScoped<IService, Service>();

            var connectionString = Environment.GetEnvironmentVariable("KeyVaultConnectionString");

            //Creazione del contesto del database
            service.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}