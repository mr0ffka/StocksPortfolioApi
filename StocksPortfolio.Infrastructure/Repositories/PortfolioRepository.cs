using MongoDB.Driver;
using StocksPortfolio.Domain.Entities;
using StocksPortfolio.Domain.Repositories;
using StocksPortfolio.Infrastructure.Extensions;
using StocksPortfolio.Infrastructure.Persistence;

namespace StocksPortfolio.Infrastructure.Repositories;

internal class PortfolioRepository(StocksPortfolioDbContext dbContext) : IPortfolioRepository
{
    public async Task<IEnumerable<Portfolio>> GetAllAsync()
    {
        return await dbContext.Portfolios
                .Find(FilterExtension.WithoutDeletedFilter<Portfolio>())
                .ToListAsync();
    }

    public async Task<Portfolio> GetByIdAsync(string id)
    {
        var filterBuilder = Builders<Portfolio>.Filter;
        var filter = filterBuilder.And(
            filterBuilder.Eq(p => p.Id, id),
            FilterExtension.WithoutDeletedFilter<Portfolio>()
        );
        return await dbContext.Portfolios.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<string> CreateAsync(Portfolio entity)
    {
        await dbContext.Portfolios.InsertOneAsync(entity);
        return entity.Id;
    }

    public Task UpdateAsync(Portfolio entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Portfolio entity)
    {
        var filter = Builders<Portfolio>.Filter.Eq(p => p.Id, entity.Id);
        var update = Builders<Portfolio>.Update.Set(p => p.DateDeletedUtc, DateTime.UtcNow);

        await dbContext.Portfolios.UpdateOneAsync(filter, update);
    }
}
