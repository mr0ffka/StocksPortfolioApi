namespace StocksPortfolio.Domain.Interfaces;

public interface ISoftDeletable
{
    public DateTime? DateDeletedUtc { get; set; }
}
