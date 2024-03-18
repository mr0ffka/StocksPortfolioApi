using StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;
using StocksPortfolio.Application.Interfaces.Integrations;
using System.Net.Http.Json;
using System.Text;

namespace StocksPortfolio.Infrastructure.Integrations.CurrencyApi;

public class CurrencyApiService(HttpClient _httpClient) : ICurrencyApiService
{
    public async Task<CurrencyApiLatestResponse> GetLatestAsync(CurrencyApiLatestRequest requestParams)
    {
        var query = new StringBuilder("latest");
        if (requestParams != null)
        {
            query.Append("?");
            if (!string.IsNullOrEmpty(requestParams.BaseCurrency))
            {
                query.Append($"base_currency={requestParams.BaseCurrency}");
            }
            if (requestParams.Currencies.Any())
            {
                query.Append("&currencies=");
                foreach (var c in requestParams.Currencies)
                {
                    query.Append($"{c},");
                }
                query.Remove(query.Length - 1, 1);
            }
        }

        var response = await _httpClient.GetAsync(query.ToString());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<CurrencyApiLatestResponse>();

        return content;
    }
}
