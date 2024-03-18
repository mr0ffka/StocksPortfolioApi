using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;
using StocksPortfolio.Application.Interfaces.Services;

namespace StocksPortfolio.Infrastructure.Extensions;

public static class HangfireJobRegistration
{
    public static void RegisterHangfireJobs(IServiceScope scope)
    {
        // 0 1 * * * 
        var currencyService = scope.ServiceProvider.GetService<ICurrencyWrapperService>();
        List<CurrencyApiLatestRequest> requests = [
            new CurrencyApiLatestRequest
            {
                BaseCurrency = "PLN",
                Currencies = new List<string> { "PLN", "EUR", "USD", "JPY", "GBP" }
            },
            new CurrencyApiLatestRequest
            {
                BaseCurrency = "EUR",
                Currencies = new List<string> { "PLN", "EUR", "USD", "JPY", "GBP" }
            },
            new CurrencyApiLatestRequest
            {
                BaseCurrency = "USD",
                Currencies = new List<string> { "PLN", "EUR", "USD", "JPY", "GBP" }
            },
            new CurrencyApiLatestRequest
            {
                BaseCurrency = "USD",
                Currencies = new List<string> { "PLN", "EUR", "USD", "JPY", "GBP" }
            },
            new CurrencyApiLatestRequest
            {
                BaseCurrency = "JPY",
                Currencies = new List<string> { "PLN", "EUR", "USD", "JPY", "GBP" }
            },
            new CurrencyApiLatestRequest
            {
                BaseCurrency = "GBP",
                Currencies = new List<string> { "PLN", "EUR", "USD", "JPY", "GBP" }
            }
        ];

        RecurringJob.AddOrUpdate(() => currencyService.SyncLatesListAsync(requests), "0 1 * * *");
    }
}
