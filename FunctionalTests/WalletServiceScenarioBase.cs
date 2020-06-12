using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using WalletService.Application;
using WalletService.Application.Extensions;
using WalletService.Application.Infrastructure;
using WalletService.Service.Infrastructure;

namespace FunctionalTests
{
    public class WalletServiceScenarioBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(WalletServiceScenarioBase))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration((r, cb) =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<WalletTestsStartup>();

            var testServer = new TestServer(hostBuilder);

            testServer.Host
                .MigrateDbContext<WalletContext>((context, services) =>
                {
                    var env = (IWebHostEnvironment)services.GetService(typeof(IWebHostEnvironment));
                    var settings = (IOptions<WalletSettings>)services.GetService(typeof(IOptions<WalletSettings>));
                    var logger = (ILogger<WalletContextSeed>)services.GetService(typeof(ILogger<WalletContextSeed>));

                    new WalletContextSeed()
                        .SeedAsync(context, env, settings, logger)
                        .Wait();
                });

            return testServer;
        }

        public static class Get
        {
            public static string Orders = "api/v1/orders";

            public static string OrderBy(int id)
            {
                return $"api/v1/orders/{id}";
            }
        }

        public static class Put
        {
            public static string CancelOrder = "api/v1/orders/cancel";
            public static string ShipOrder = "api/v1/orders/ship";
        }
    }
}