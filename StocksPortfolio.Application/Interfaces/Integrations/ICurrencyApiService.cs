using StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;

namespace StocksPortfolio.Application.Interfaces.Integrations;

public interface ICurrencyApiService
{
    Task<CurrencyApiLatestResponse> GetLatestAsync(CurrencyApiLatestRequest request);
}
