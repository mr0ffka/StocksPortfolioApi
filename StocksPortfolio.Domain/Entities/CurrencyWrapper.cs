using StocksPortfolio.Domain.Entities.Common;

namespace StocksPortfolio.Domain.Entities;

public class CurrencyWrapper : IEntity
{
    public required string Id { get; set; }
    public DateTime? DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public string BaseCurrency { get; set; } = default!;
    public IEnumerable<Currency> Currencies { get; set; } = [];
}
