namespace StocksPortfolio.Domain.Entities;

public class Stock
{
    public string Ticker { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public int NumberOfShares { get; set; }
}
