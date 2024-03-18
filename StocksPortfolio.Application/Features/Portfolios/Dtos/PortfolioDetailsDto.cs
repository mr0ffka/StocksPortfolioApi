using StocksPortfolio.Application.Features.Stocks.Dtos;
using StocksPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Application.Features.Portfolios.Dtos;

public class PortfolioDetailsDto
{
    public required string Id { get; set; }
    public DateTime? DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public DateTime? DateDeletedUtc { get; set; }
    public string BaseCurrency { get; set; } = default!;
    public decimal TotalValue { get; set; }
    public ICollection<StockDto> Stocks { get; set; } = [];
}
