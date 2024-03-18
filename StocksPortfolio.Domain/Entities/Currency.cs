namespace StocksPortfolio.Domain.Entities;

public class Currency
{
    public string Code { get; set; } = default!;
    public decimal Value { get; set; }
}
