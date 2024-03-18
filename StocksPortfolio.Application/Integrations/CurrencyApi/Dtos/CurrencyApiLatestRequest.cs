namespace StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;

public class CurrencyApiLatestRequest
{
    public string BaseCurrency { get; set; } = default!;
    public IEnumerable<string> Currencies { get; set; } = [];
}
