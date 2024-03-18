using AutoMapper;
using StocksPortfolio.Application.Exceptions;
using StocksPortfolio.Application.Features.Portfolios.Dtos;
using StocksPortfolio.Application.Interfaces.Services;
using StocksPortfolio.Domain.Entities;
using StocksPortfolio.Domain.Repositories;

namespace StocksPortfolio.Application.Features.Portfolios;

public class PortfolioService(
    IPortfolioRepository portfolioRepository, 
    IStockService stockService,
    IMapper mapper) 
    : IPortfolioService
{
    public Task<string> CreateAsync(PortfolioCreateDto entity)
    {
        var mapped = mapper.Map<Portfolio>(entity);
        return portfolioRepository.CreateAsync(mapped);
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await portfolioRepository.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException(typeof(Portfolio).ToString(), id);

        await portfolioRepository.DeleteAsync(entity);
    }

    public async Task<IReadOnlyList<PortfolioDetailsDto>> GetAllAsync()
    {
        var entities = await portfolioRepository.GetAllAsync();
        var mapped = mapper.Map<List<PortfolioDetailsDto>>(entities);

        return mapped.AsReadOnly();
    }

    public async Task<PortfolioDetailsDto> GetByIdAsync(string id, string? currency = null)
    {
        var entity = await portfolioRepository.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException(typeof(Portfolio).ToString(), id);

        var mapped = mapper.Map<PortfolioDetailsDto>(entity);

        if (!string.IsNullOrEmpty(currency))
        {
            mapped.BaseCurrency = currency;
        }

        foreach (var stock in mapped.Stocks)
        {
            mapped.TotalValue += await stockService.CalculateStockTotalValue(stock, mapped.BaseCurrency);
        }

        return mapped;
    }

    public async Task<decimal> GetValueAsync(string id, string currency)
    {
        var entity = await portfolioRepository.GetByIdAsync(id);
        var totalValue = 0m;

        if (entity == null)
            throw new NotFoundException(typeof(Portfolio).ToString(), id);

        var mapped = mapper.Map<PortfolioDetailsDto>(entity);

        foreach (var stock in mapped.Stocks)
        {
            totalValue += await stockService.CalculateStockTotalValue(stock, currency);
        }

        return totalValue;
    }

    public Task UpdateAsync(Portfolio entity)
    {
        throw new NotImplementedException();
    }
}
