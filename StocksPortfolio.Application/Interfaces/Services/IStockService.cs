using StocksPortfolio.Application.Features.Stocks.Dtos;

namespace StocksPortfolio.Application.Interfaces.Services;

public interface IStockService
{
    Task<decimal> CalculateStockTotalValue(StockDto stock, string baseCurrency = "USD");
}
