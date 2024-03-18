using StocksPortfolio.Application.Features.Stocks.Dtos;
using StocksPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Application.Features.Portfolios.Dtos;

public class PortfolioCreateDto
{
    public float TotalValue { get; set; }
    public ICollection<StockDto> Stocks { get; set; } = [];
}
