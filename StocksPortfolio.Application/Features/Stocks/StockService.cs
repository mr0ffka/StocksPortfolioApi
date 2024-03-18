using StocksPortfolio.Application.Features.Stocks.Dtos;
using StocksPortfolio.Application.Interfaces.Services;
using StocksPortfolio.Domain.Exceptions;
using StocksService;

namespace StocksPortfolio.Application.Features.Stocks;

public class StockService(
    IStocksService stockPriceService,
    ICurrencyWrapperService currencyWrapperService
    ) : IStockService
{
    public async Task<decimal> CalculateStockTotalValue(StockDto stock, string baseCurrency = "USD")
    {
        var priceModel = await stockPriceService.GetStockPrice(stock.Ticker);
        var priceExchangeRate = await currencyWrapperService.GetByBaseCurrency(priceModel.Currency);
        var stockCurrencyExchangeRate = await currencyWrapperService.GetByBaseCurrency(stock.Currency);

        if (!priceExchangeRate.Currencies.Any(c => c.Code == baseCurrency))
            throw new NotFoundException($"Exchange rate from {priceModel.Currency} to {baseCurrency} not found", $"{baseCurrency}{priceModel.Currency}");

        var exchangeRatePrice = priceExchangeRate.Currencies.FirstOrDefault(c => c.Code == stock.Currency);
        var exchangeRateStock = stockCurrencyExchangeRate.Currencies.FirstOrDefault(c => c.Code == baseCurrency);

        var perOneToStockCurrency = exchangeRatePrice != null ? priceModel.Price * exchangeRatePrice.Value : 0;
        var perOneToBaseCurrency = exchangeRateStock != null ? perOneToStockCurrency * exchangeRateStock.Value : 0;

        return perOneToBaseCurrency * stock.NumberOfShares;
    }
}
