using AutoMapper;
using StocksPortfolio.Application.Features.Portfolios.Dtos;
using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Application.Features.Portfolios;

public class PortfolioMappingProfile : Profile
{
    public PortfolioMappingProfile()
    {
        CreateMap<PortfolioCreateDto, Portfolio>();
        CreateMap<PortfolioDetailsDto, Portfolio>()
            .ReverseMap();
    }
}
