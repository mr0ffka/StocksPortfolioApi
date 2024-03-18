using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Application.Features.Currencies.Dtos;

public class CurrencyWrapperDetailsDto
{
    public required string Id { get; set; }
    public DateTime? DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public string BaseCurrency { get; set; } = default!;
    public IEnumerable<Currency> Currencies { get; set; } = [];
}
