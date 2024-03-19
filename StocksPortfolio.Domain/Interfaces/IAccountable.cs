namespace StocksPortfolio.Domain.Entities.Common;

public interface IAccountable
{
    public DateTime? DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
}
