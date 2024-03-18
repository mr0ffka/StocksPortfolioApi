using StocksPortfolio.Domain.Entities;

namespace StocksPortfolio.Domain.Repositories;

public interface ICurrencyWrapperRepository : IGenericRepository<CurrencyWrapper>
{
    Task<CurrencyWrapper> GetByBaseCurrency(string code);
}
