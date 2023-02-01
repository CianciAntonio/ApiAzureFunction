using EntityFrameworkClassLibrary;
using EntityFrameworkClassLibrary.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json.Serialization;

[assembly: FunctionsStartup(typeof(ApiFunctionWithRepositoryPattern.StartUp))]

namespace ApiFunctionWithRepositoryPattern
{
    public class StartUp : FunctionsStartup
    {  
        public override void Configure(IFunctionsHostBuilder service)
        {
            //service.Services.AddControllers().AddJsonOptions(opts =>
            //                                    {
            //                                        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //                                        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
            //                                    });

            service.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            service.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            service.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}