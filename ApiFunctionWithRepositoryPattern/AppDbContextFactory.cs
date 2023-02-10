using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EntityFrameworkClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFunctionWithRepositoryPattern
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //        .Build();
            //var connectionString = configuration.GetValue<string>("KeyVaultConnectionString");

            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            var kvaulturi = configuration.GetValue<string>("KeyVaultUri");
            var secretClient = new SecretClient(new Uri(kvaulturi), new DefaultAzureCredential());
            var connectionString = secretClient.GetSecret("AzureConnectionString-AC").Value.Value;


            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
