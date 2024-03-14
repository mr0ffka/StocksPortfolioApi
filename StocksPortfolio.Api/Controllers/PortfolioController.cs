using StocksPortfolio.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Text.Json;

namespace StocksPortfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly DataProviderService _dataService;

        private class Quote
        {
            public bool success { get; set; }
            public string terms { get; set; }
            public string privacy { get; set; }
            public int timestamp { get; set; }
            public string source { get; set; }
            public Dictionary<string, decimal> quotes { get; set; }
        }

        public PortfolioController()
        {
            _dataService = new DataProviderService();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var portfolio = _dataService.GetPortfolio(ObjectId.Parse(id)).Result;
            return Ok(portfolio);
        }

        [HttpGet("/value")]
        public IActionResult GetTotalPortfolioValue(string portfolioId, string currency = "USD")
        {
            var portfolio = _dataService.GetPortfolio(ObjectId.Parse(portfolioId)).Result;
            var totalAmount = 0m;
            var stockService = new StocksService.StocksService();
            var apiAccessKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            using (var httpClient = new HttpClient { BaseAddress = new Uri("http://api.currencylayer.com/") })
            {
                // Docs: https://currencylayer.com/documentation
                var foo = httpClient.GetAsync($"live?access_key={apiAccessKey}").Result;
                var data = JsonSerializer.DeserializeAsync<Quote>(foo.Content.ReadAsStream()).Result;

                foreach (var stock in portfolio.Stocks)
                {
                    if (stock.Currency == currency)
                    {
                        totalAmount += stockService.GetStockPrice(stock.Ticker).Result.Price * stock.NumberOfShares;
                    }
                    else
                    {
                        if (currency == "USD")
                        {
                            var stockPrice = stockService.GetStockPrice(stock.Ticker).Result.Price;
                            var rateUsd = data.quotes["USD" + stock.Currency];
                            totalAmount += stockPrice / rateUsd * stock.NumberOfShares;
                        }
                        else
                        {
                            var stockPrice = stockService.GetStockPrice(stock.Ticker).Result.Price;
                            var rateUsd = data.quotes["USD" + stock.Currency];
                            var amount = stockPrice / rateUsd * stock.NumberOfShares;
                            var targetRateUsd = data.quotes["USD" + currency];
                            totalAmount += amount * targetRateUsd;
                        }
                    }
                }
            }

            return Ok(totalAmount);
        }

        [HttpGet("/delete")]
        public IActionResult DeletePortfolio(string portfolioId)
        {
            var dataService = new DataProviderService();
            dataService.DeletePortfolio(ObjectId.Parse(portfolioId));
            return Ok();
        }
    }
}
