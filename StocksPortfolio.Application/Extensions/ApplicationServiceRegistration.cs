using Microsoft.Extensions.DependencyInjection;
using StocksPortfolio.Application.Features.Portfolios;
using StocksPortfolio.Application.Features.Currencies;
using System.Reflection;
using StocksPortfolio.Application.Interfaces.Services;
using StocksPortfolio.Application.Features.Stocks;
using StocksService;

namespace StocksPortfolio.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped(typeof(IPortfolioService), typeof(PortfolioService));
        services.AddScoped(typeof(IStockService), typeof(StockService));
        services.AddScoped(typeof(IStocksService), typeof(StocksService.StocksService));
        services.AddScoped(typeof(ICurrencyWrapperService), typeof(CurrencyWrapperService));

        return services;
    }
}
