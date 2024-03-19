using FluentAssertions;
using Moq;
using StocksPortfolio.Application.Features.Currencies.Dtos;
using StocksPortfolio.Application.Features.Stocks.Dtos;
using StocksPortfolio.Application.Interfaces.Services;
using StocksPortfolio.Domain.Entities;
using StocksPortfolio.Domain.Exceptions;
using StocksService;
using Xunit;

namespace StocksPortfolio.Application.Features.Stocks.Tests;

public class StockServiceTests
{
    private readonly Mock<IStocksService> _mockStockPriceService;
    private readonly Mock<ICurrencyWrapperService> _mockCurrencyWrapperService;
    private readonly StockService _stockService;

    public StockServiceTests()
    {
        _mockStockPriceService = new Mock<IStocksService>();
        _mockCurrencyWrapperService = new Mock<ICurrencyWrapperService>();
        _stockService = new StockService(_mockStockPriceService.Object, _mockCurrencyWrapperService.Object);
    }

    [Fact]
    public async Task CalculateStockTotalValue_WhenAllCorrectValues_ReturnsCorrectValue()
    {
        var stock = new StockDto { Ticker = "AAPL", NumberOfShares = 10, Currency = "EUR" };
        var baseCurrency = "USD";

        _mockStockPriceService.Setup(service => service.GetStockPrice(stock.Ticker))
            .ReturnsAsync(new StockModel { Price = 100m, Currency = "USD" });

        _mockCurrencyWrapperService.Setup(service => service.GetByBaseCurrency("USD"))
            .ReturnsAsync(new CurrencyWrapperDetailsDto
            {
                Id = "<wrapper-id-1>",
                BaseCurrency = "USD",
                Currencies = new[]
                {
                    new Currency { Code = "EUR", Value = 0.9m },
                    new Currency { Code = "USD", Value = 1.0m }
                }
            });

        _mockCurrencyWrapperService.Setup(service => service.GetByBaseCurrency("EUR"))
            .ReturnsAsync(new CurrencyWrapperDetailsDto
            {
                Id = "<wrapper-id-2>",
                BaseCurrency = "EUR",
                Currencies = new[]
                {
                    new Currency { Code = "USD", Value = 1.1m },
                    new Currency { Code = "EUR", Value = 1.0m },
                }
            });

        decimal expectedValue = 1000m;

        var actualValue = await _stockService.CalculateStockTotalValue(stock, baseCurrency);

        actualValue.Should().Be(expectedValue);
    }

    [Fact()]
    public async Task CalculateStockTotalValue_WhenExchangeRateNotFound_ThrowsNotFoundException()
    {
        var stock = new StockDto { Ticker = "AAPL", NumberOfShares = 10, Currency = "EUR" };
        var baseCurrency = "USD";
        var stockPrice = 100m;

        _mockStockPriceService.Setup(service => service.GetStockPrice(stock.Ticker))
            .ReturnsAsync(new StockModel { Price = stockPrice, Currency = "EUR" });

        _mockCurrencyWrapperService.Setup(service => service.GetByBaseCurrency("EUR"))
            .ReturnsAsync(new CurrencyWrapperDetailsDto
            {
                Id = "<wrapper-id-1>",
                BaseCurrency = "EUR",
                Currencies = new Currency[] { }
            });

        Func<Task> act = async () => await _stockService.CalculateStockTotalValue(stock, baseCurrency);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact()]
    public async Task CalculateStockTotalValue_CurrencyWrapperServiceFailure_ThrowsException()
    {
        var stock = new StockDto { Ticker = "AAPL", NumberOfShares = 10, Currency = "EUR" };

        _mockStockPriceService.Setup(service => service.GetStockPrice(stock.Ticker))
            .ReturnsAsync(new StockModel { Price = 100m, Currency = "EUR" });

        _mockCurrencyWrapperService.Setup(service => service.GetByBaseCurrency(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Failed to retrieve currency exchange rate"));

        Func<Task> act = async () => await _stockService.CalculateStockTotalValue(stock, "USD");

        await act.Should().ThrowAsync<Exception>();
    }
}