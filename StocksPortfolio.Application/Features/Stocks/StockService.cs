using StocksPortfolio.Application.Features.Stocks.Dtos;
using StocksPortfolio.Application.Interfaces.Services;
using StocksPortfolio.Domain.Entities;
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
        var priceCurrencyDbModel = await currencyWrapperService.GetByBaseCurrency(priceModel.Currency);

        if (priceCurrencyDbModel is null)
            throw new NotFoundException(nameof(CurrencyWrapper), priceModel.Currency);

        var exchangeRate = priceCurrencyDbModel!.Currencies.FirstOrDefault(r => r.Code == baseCurrency);

        if (exchangeRate is null)
            throw new NotFoundException($"{nameof(Currency)} in {nameof(CurrencyWrapper)}", $"{priceModel.Currency}{baseCurrency}");

        return priceModel.Price * exchangeRate!.Value * stock.NumberOfShares;
    }
}
