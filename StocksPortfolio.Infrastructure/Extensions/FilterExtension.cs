using MongoDB.Driver;
using StocksPortfolio.Domain.Interfaces;

namespace StocksPortfolio.Infrastructure.Extensions;

internal static class FilterExtension
{
    internal static FilterDefinition<T> WithoutDeletedFilter<T>() where T : ISoftDeletable
    {
        var filterBuilder = Builders<T>.Filter;
        var filter = filterBuilder.Or(
            filterBuilder.Gt(p => p.DateDeletedUtc, DateTime.UtcNow),
            filterBuilder.Eq(p => p.DateDeletedUtc, null)
        );

        return filter;
    }

}
