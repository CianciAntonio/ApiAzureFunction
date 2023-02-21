using EntityFrameworkClassLibrary;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;
using ApiFunctionWithRepositoryPattern.LogicBusiness;
using Dtos;
using Mapster;
using MapsterMapper;

namespace ApiFunctionWithRepositoryPattern
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Configurazione di mapster
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();

            //Configurazione di Swagger
            builder.Services.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts => {
                opts.AddCodeParameter = true;
                opts.Documents = new[] {
                    new SwaggerDocument {
                        Name = "v1",
                            Title = "Swagger document",
                            Description = "Integrate Swagger UI With Azure Functions",
                            Version = "v2"
                    }
                };
                opts.ConfigureSwaggerGen = x => {
                    x.CustomOperationIds(apiDesc => {
                        return apiDesc.TryGetMethodInfo(out MethodInfo mInfo) ? mInfo.Name : default(Guid).ToString();
                    });
                };
            });

            //Evita l'errore di loop
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //Dependency Injection basata su Scoped
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IService, Service>();

            var connectionString = Environment.GetEnvironmentVariable("KeyVaultConnectionString");

            //Creazione del contesto del database
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}