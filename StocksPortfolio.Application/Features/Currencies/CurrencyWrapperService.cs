using AutoMapper;
using StocksPortfolio.Application.Exceptions;
using StocksPortfolio.Application.Features.Currencies.Dtos;
using StocksPortfolio.Application.Integrations.CurrencyApi.Dtos;
using StocksPortfolio.Application.Interfaces.Integrations;
using StocksPortfolio.Application.Interfaces.Services;
using StocksPortfolio.Domain.Entities;
using StocksPortfolio.Domain.Entities.Common;
using StocksPortfolio.Domain.Repositories;

namespace StocksPortfolio.Application.Features.Currencies;

public class CurrencyWrapperService(
    ICurrencyApiService currencyApiService, 
    ICurrencyWrapperRepository currencyWrapperRepository,
    IMapper mapper
    ) : ICurrencyWrapperService
{
    public async Task<CurrencyWrapperDetailsDto> GetByBaseCurrency(string code)
    {
        var entity = await currencyWrapperRepository.GetByBaseCurrency(code);
        if (entity == null)
            throw new NotFoundException(typeof(CurrencyWrapper).ToString(), code);

        var mapped = mapper.Map<CurrencyWrapperDetailsDto>(entity);

        return mapped;
    }

    public async Task SyncLatesListAsync(IEnumerable<CurrencyApiLatestRequest> requests)
    {
        foreach (var r in requests)
        {
            await SyncLatesAsync(r);
        }
    }

    public async Task SyncLatesAsync(CurrencyApiLatestRequest request)
    {
        var response = await currencyApiService.GetLatestAsync(request);
        if (response != null && response.Data.Any())
        {
            var values = response.Data.Values.ToList();
            var newEntityValues = mapper.Map<IEnumerable<Currency>>(values);

            var entity = await currencyWrapperRepository.GetByBaseCurrency(request.BaseCurrency);
            if (entity != null)
            {
                entity.Currencies = newEntityValues;
                entity.DateModifiedUtc = DateTime.UtcNow;
                await currencyWrapperRepository.UpdateAsync(entity);
            }
            else
            {
                var newEntity = new CurrencyWrapperCreateDto();
                newEntity.BaseCurrency = request.BaseCurrency;
                newEntity.DateModifiedUtc = DateTime.UtcNow;
                newEntity.DateCreatedUtc = DateTime.UtcNow;
                newEntity.Currencies = newEntityValues;
                var mapped = mapper.Map<CurrencyWrapper>(newEntity);

                await currencyWrapperRepository.CreateAsync(mapped);
            }
        }

    }
}
