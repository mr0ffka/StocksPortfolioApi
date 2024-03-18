using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Application.Features.Stocks.Dtos;

public class StockDto
{
    public string Ticker { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public int NumberOfShares { get; set; }

}
