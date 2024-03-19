using StocksPortfolio.Domain.Entities.Common;
using StocksPortfolio.Domain.Interfaces;

namespace StocksPortfolio.Domain.Entities;

public class Portfolio : IEntity, IAccountable, ISoftDeletable
{
    public required string Id { get; set; }
    public DateTime? DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public DateTime? DateDeletedUtc { get; set; }
    public string BaseCurrency { get; set; } = default!;
    public decimal TotalValue { get; set; }
    public ICollection<Stock> Stocks { get; set; } = [];
}
