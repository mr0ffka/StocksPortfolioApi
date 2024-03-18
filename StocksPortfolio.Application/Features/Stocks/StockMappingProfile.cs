using AutoMapper;
using StocksPortfolio.Application.Features.Portfolios.Dtos;
using StocksPortfolio.Application.Features.Stocks.Dtos;
using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Application.Features.Stocks;

public class StockMappingProfile : Profile
{
    public StockMappingProfile()
    {
        CreateMap<StockDto, Stock>()
            .ReverseMap();
    }
}
