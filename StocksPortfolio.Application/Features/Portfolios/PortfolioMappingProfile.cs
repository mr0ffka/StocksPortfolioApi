using AutoMapper;
using StocksPortfolio.Application.Features.Portfolios.Dtos;
using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Application.Features.Portfolios;

public class PortfolioMappingProfile : Profile
{
    public PortfolioMappingProfile()
    {
        CreateMap<PortfolioCreateDto, Portfolio>()
            .ForMember(d => d.DateCreatedUtc, o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.DateModifiedUtc, o => o.MapFrom(s => DateTime.UtcNow));
        CreateMap<PortfolioDetailsDto, Portfolio>()
            .ReverseMap();
    }
}
