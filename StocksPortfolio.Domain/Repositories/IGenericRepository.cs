using StocksPortfolio.Domain.Entities.Common;

namespace StocksPortfolio.Domain.Repositories;

public interface IGenericRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task<string> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
