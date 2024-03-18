using AutoMapper;
using StocksPortfolio.Application.Features.Currencies.Dtos;
using StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;
using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Application.Features.Portfolios;

public class CurrencyMappingProfile : Profile
{
    public CurrencyMappingProfile()
    {
        CreateMap<CurrencyWrapperCreateDto, CurrencyWrapper>()
            .ReverseMap();
        CreateMap<CurrencyWrapperDetailsDto, CurrencyWrapper>()
            .ReverseMap();
        CreateMap<CurrencyApiCurrencyModel, Currency>()
            .ReverseMap();
    }
}
