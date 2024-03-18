using MongoDB.Driver;
using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Infrastructure.Persistence;

public class StocksPortfolioDbContext
{
    private readonly IMongoDatabase database;

    public StocksPortfolioDbContext(IMongoClient client)
    {
        this.database = client.GetDatabase("portfolioDb");
    }

    public IMongoCollection<Portfolio> Portfolios => database.GetCollection<Portfolio>("Portfolios");
    public IMongoCollection<CurrencyWrapper> Currencies => database.GetCollection<CurrencyWrapper>("Currencies");

}