using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WalletService.Service.Infrastructure;

namespace WalletService.API.Infrastructure.Infrastructure.Factories
{
    public class WalletDbContextFactory : IDesignTimeDbContextFactory<WalletContext>
    {
        public WalletContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<WalletContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("WalletService.API"));

            return new WalletContext(optionsBuilder.Options);
        }
    }
}
