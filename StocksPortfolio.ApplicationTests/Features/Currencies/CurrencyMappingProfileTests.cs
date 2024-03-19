using AutoMapper;
using FluentAssertions;
using StocksPortfolio.Application.Features.Currencies.Dtos;
using StocksPortfolio.Domain.Entities;
using Xunit;

namespace StocksPortfolio.Application.Features.Portfolios.Tests;

public class CurrencyMappingProfileTests
{
    private IMapper _mapper;
    public CurrencyMappingProfileTests()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CurrencyMappingProfile>();
        });

        _mapper = conf.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForCurrencyWrapperToCurrencyWraperDetailsDto_MapsCorrectly()
    {
        var currencyWrapper = new CurrencyWrapper()
        {
            Id = "currency-id",
            DateCreatedUtc = new DateTime(2024, 3, 18, 17, 23, 23),
            DateModifiedUtc = new DateTime(2024, 3, 18, 17, 23, 23),
            BaseCurrency = "USD",
            Currencies =
            [
                new Currency
                {
                    Code = "PLN",
                    Value = 5.5m
                },
                new Currency
                {
                    Code = "USD",
                    Value = 1.0m
                },
            ]
        };

        var currencyWrapperDto = _mapper.Map<CurrencyWrapperDetailsDto>(currencyWrapper);

        currencyWrapperDto.Should().NotBeNull();
        currencyWrapperDto.Id.Should().Be(currencyWrapper.Id);
        currencyWrapperDto.DateCreatedUtc.Should().Be(currencyWrapper.DateCreatedUtc);
        currencyWrapperDto.DateModifiedUtc.Should().Be(currencyWrapper.DateModifiedUtc);
        currencyWrapperDto.BaseCurrency.Should().Be(currencyWrapper.BaseCurrency);
        currencyWrapperDto.Currencies.Count().Should().Be(currencyWrapper.Currencies.Count());
        currencyWrapperDto.Currencies.Should().BeEquivalentTo(currencyWrapper.Currencies);
    }

    [Fact()]
    public void CreateMap_ForCurrencyWrapperCreateDtoToCurrencyWraper_MapsCorrectly()
    {
        var currencyWrapperCreateDto = new CurrencyWrapperCreateDto()
        {
            DateCreatedUtc = new DateTime(2024, 3, 18, 17, 23, 23),
            DateModifiedUtc = new DateTime(2024, 3, 18, 17, 23, 23),
            BaseCurrency = "USD",
            Currencies =
            [
                new Currency
                {
                    Code = "PLN",
                    Value = 5.5m
                },
                new Currency
                {
                    Code = "USD",
                    Value = 1.0m
                },
            ]
        };

        var currencyWrapper = _mapper.Map<CurrencyWrapper>(currencyWrapperCreateDto);

        currencyWrapper.Should().NotBeNull();
        currencyWrapper.Id.Should().BeNull();
        currencyWrapper.DateCreatedUtc.Should().Be(currencyWrapperCreateDto.DateCreatedUtc);
        currencyWrapper.DateModifiedUtc.Should().Be(currencyWrapperCreateDto.DateModifiedUtc);
        currencyWrapper.BaseCurrency.Should().Be(currencyWrapperCreateDto.BaseCurrency);
        currencyWrapper.Currencies.Count().Should().Be(currencyWrapperCreateDto.Currencies.Count());
        currencyWrapper.Currencies.Should().BeEquivalentTo(currencyWrapperCreateDto.Currencies);
    }
}