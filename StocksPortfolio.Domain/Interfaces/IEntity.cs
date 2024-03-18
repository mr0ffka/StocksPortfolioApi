namespace StocksPortfolio.Domain.Entities.Common;

public interface IEntity
{
    public string Id { get; set; }
    public DateTime? DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
}
