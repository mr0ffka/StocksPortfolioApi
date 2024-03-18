using StocksPortfolio.Application.Features.Portfolios.Dtos;
using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Application.Interfaces.Services;

public interface IPortfolioService
{
    Task<IReadOnlyList<PortfolioDetailsDto>> GetAllAsync();
    Task<PortfolioDetailsDto> GetByIdAsync(string id, string? currency = null);
    Task<decimal> GetValueAsync(string id, string currency);
    Task<string> CreateAsync(PortfolioCreateDto entity);
    Task UpdateAsync(Portfolio entity);
    Task DeleteAsync(string id);
}
