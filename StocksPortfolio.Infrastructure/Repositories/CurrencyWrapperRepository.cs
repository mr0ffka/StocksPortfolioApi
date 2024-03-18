using MongoDB.Bson;
using MongoDB.Driver;
using StocksPortfolio.Domain.Entities;
using StocksPortfolio.Domain.Repositories;
using StocksPortfolio.Infrastructure.Persistence;

namespace StocksPortfolio.Infrastructure.Repositories;

internal class CurrencyWrapperRepository(StocksPortfolioDbContext dbContext) : ICurrencyWrapperRepository
{
    public async Task<string> CreateAsync(CurrencyWrapper entity)
    {
        await dbContext.Currencies.InsertOneAsync(entity);
        return entity.Id;
    }

    public async Task DeleteAsync(CurrencyWrapper entity)
    {
        var filter = Builders<CurrencyWrapper>.Filter.Eq(p => p.Id, entity.Id);

        await dbContext.Currencies.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<CurrencyWrapper>> GetAllAsync()
    {
        return await dbContext.Currencies
            .Find(new BsonDocument())
            .ToListAsync();
    }

    public async Task<CurrencyWrapper> GetByBaseCurrency(string code)
    {
        var filterBuilder = Builders<CurrencyWrapper>.Filter;
        var filter = filterBuilder.And(
            filterBuilder.Eq(p => p.BaseCurrency, code)
        );
        return await dbContext.Currencies.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<CurrencyWrapper> GetByIdAsync(string id)
    {
        var filterBuilder = Builders<CurrencyWrapper>.Filter;
        var filter = filterBuilder.And(
            filterBuilder.Eq(p => p.Id, id)
        );
        return await dbContext.Currencies.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(CurrencyWrapper entity)
    {
        var filter = Builders<CurrencyWrapper>.Filter.Eq(p => p.Id, entity.Id);
        var update = Builders<CurrencyWrapper>.Update
            .Set(p => p.DateModifiedUtc, entity.DateModifiedUtc)
            .Set(p => p.Currencies, entity.Currencies)
            .Set(p => p.BaseCurrency, entity.BaseCurrency)
            .Set(p => p.DateCreatedUtc, entity.DateCreatedUtc);

        await dbContext.Currencies.UpdateOneAsync(filter, update);
    }
}
