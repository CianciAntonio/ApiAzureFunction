using EntityFrameworkClassLibrary;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

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

            //Creazione del contesto del database
            service.Services.AddDbContext<AppDbContext>();
        }
    }
}