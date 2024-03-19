using AutoMapper;
using FluentAssertions;
using StocksPortfolio.Application.Features.Portfolios.Dtos;
using StocksPortfolio.Application.Features.Stocks;
using StocksPortfolio.Application.Features.Stocks.Dtos;
using StocksPortfolio.Domain.Entities;
using Xunit;

namespace StocksPortfolio.Application.Features.Portfolios.Tests;

public class PortfolioMappingProfileTests
{
    private IMapper _mapper;
    public PortfolioMappingProfileTests()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PortfolioMappingProfile>();
            cfg.AddProfile<StockMappingProfile>();
        });

        _mapper = conf.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForPortfolioCreateDtoToPortfolio_MapsCorrectly()
    {
        var dto = new PortfolioCreateDto()
        {
            TotalValue = 1234.5m,
            BaseCurrency = "USD",
            Stocks = [
                new StockDto
                {
                    Ticker = "USDPLN",
                    Currency = "PLN",
                    NumberOfShares = 54321
                }
            ]
        };

        var portfolio = _mapper.Map<Portfolio>(dto);

        portfolio.Should().NotBeNull();
        portfolio.Id.Should().BeNull();
        portfolio.DateCreatedUtc.Should().NotBeNull();
        portfolio.DateModifiedUtc.Should().NotBeNull();
        portfolio.DateDeletedUtc.Should().BeNull();
        portfolio.BaseCurrency.Should().Be(dto.BaseCurrency);
        portfolio.TotalValue.Should().Be(dto.TotalValue);
        portfolio.Stocks.Count().Should().Be(dto.Stocks.Count());
        portfolio.Stocks.Should().BeEquivalentTo(dto.Stocks);
    }
}