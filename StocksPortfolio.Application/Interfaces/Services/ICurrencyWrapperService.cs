using StocksPortfolio.Application.Features.Currencies.Dtos;
using StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;

namespace StocksPortfolio.Application.Interfaces.Services;

public interface ICurrencyWrapperService
{
    Task SyncLatesAsync(CurrencyApiLatestRequest request);
    Task SyncLatesListAsync(IEnumerable<CurrencyApiLatestRequest> requests);
    Task<CurrencyWrapperDetailsDto> GetByBaseCurrency(string code);
}
