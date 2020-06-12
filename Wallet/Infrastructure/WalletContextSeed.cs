using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Service.Infrastructure;

namespace WalletService.Application.Infrastructure
{
    public class WalletContextSeed
    {
        public async Task SeedAsync(WalletContext context, IWebHostEnvironment env, IOptions<WalletSettings> settings, ILogger<WalletContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(WalletContextSeed));

            await policy.ExecuteAsync(async () =>
            {

                var contentRootPath = env.ContentRootPath;


                using (context)
                {
                    context.Database.Migrate();


                    if (!context.PaymentMethods.Any())
                    {
                        context.PaymentMethods.AddRange(GetPredefinedPaymentMethod());
                    }

                    await context.SaveChangesAsync();
                }
            });
        }


        private IEnumerable<PaymentMethod> GetPredefinedPaymentMethod()
        {
            return new List<PaymentMethod>()
            {
                PaymentMethod.Cash,
                PaymentMethod.GooglePay,
                PaymentMethod.BitCoin
            };
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<WalletContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
