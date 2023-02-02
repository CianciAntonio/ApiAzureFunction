using EntityFrameworkClassLibrary;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json.Serialization;

[assembly: FunctionsStartup(typeof(ApiFunctionWithRepositoryPattern.Startup))]

namespace ApiFunctionWithRepositoryPattern
{
    public class Startup : FunctionsStartup
    {  
        public override void Configure(IFunctionsHostBuilder service)
        {
            //Meteodo alternativo per risolvere errore loop con System.Text.Json
            //service.Services.AddControllers().AddJsonOptions(opts =>
            //                                    {
            //                                        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //                                        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
            //                                    });

            //Evita l'errore di loop
            service.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //Dependency Injection basata su Scoped
            service.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Creazione del contesto del database
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            service.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}