namespace StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;

public class CurrencyApiLatestResponse
{
    public CurrencyApiMetaModel Meta { get; set; }
    public Dictionary<string, CurrencyApiCurrencyModel> Data { get; set; }
}