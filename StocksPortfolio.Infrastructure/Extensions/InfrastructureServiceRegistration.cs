using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using StocksPortfolio.Application.Interfaces.Integrations;
using StocksPortfolio.Domain.Entities;
using StocksPortfolio.Domain.Repositories;
using StocksPortfolio.Infrastructure.Integrations.CurrencyApi;
using StocksPortfolio.Infrastructure.Persistence;
using StocksPortfolio.Infrastructure.Repositories;

namespace StocksPortfolio.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    private static readonly MongoDbRunner _runner = MongoDbRunner.Start();

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        BsonClassMapper();

        _runner.Import("portfolioDb", "Portfolios", Path.Combine("Data", "portfolios.json"), true);
        _runner.Import("portfolioDb", "Currencies", Path.Combine("Data", "currencies.json"), true);

        // Hangfire Client
        services.AddHangfire(conf => conf
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage(configuration.GetConnectionString("HangfireConnection")));

        // Hangfire Server
        services.AddHangfireServer();

        services.AddSingleton<IMongoClient>(new MongoClient(_runner.ConnectionString));
        services.AddScoped<StocksPortfolioDbContext>();

        services.AddHttpClient<ICurrencyApiService, CurrencyApiService>(client =>
        {
            client.BaseAddress = new Uri("https://api.currencyapi.com/v3/");
            client.DefaultRequestHeaders.Add("apikey", configuration["CurrenciesApiKey"]);
        });

        services.AddScoped(typeof(IPortfolioRepository), typeof(PortfolioRepository));
        services.AddScoped(typeof(ICurrencyWrapperRepository), typeof(CurrencyWrapperRepository));

        return services;
    }

    private static void BsonClassMapper()
    {
        BsonSerializer.RegisterIdGenerator(
            typeof(string),
            StringObjectIdGenerator.Instance
        );

        BsonClassMap.RegisterClassMap<Stock>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapProperty(c => c.Ticker).SetElementName("ticker");
            cm.MapProperty(c => c.Currency).SetElementName("currency");
            cm.MapProperty(c => c.NumberOfShares).SetElementName("numberOfShares");

        });

        BsonClassMap.RegisterClassMap<Portfolio>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapIdMember(c => c.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapProperty(c => c.TotalValue).SetElementName("totalValue");
            cm.MapProperty(c => c.DateCreatedUtc).SetElementName("dateCreatedUtc");
            cm.MapProperty(c => c.DateModifiedUtc).SetElementName("dateModifiedUtc");
            cm.MapProperty(c => c.DateDeletedUtc).SetElementName("dateDeletedUtc");
            cm.MapProperty(c => c.BaseCurrency).SetElementName("baseCurrency");
            cm.MapProperty(c => c.Stocks).SetElementName("stocks");

        });

        BsonClassMap.RegisterClassMap<Currency>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapProperty(c => c.Code).SetElementName("code");
            cm.MapProperty(c => c.Value).SetElementName("value");
        });

        BsonClassMap.RegisterClassMap<CurrencyWrapper>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapIdMember(c => c.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapProperty(c => c.DateCreatedUtc).SetElementName("dateCreatedUtc");
            cm.MapProperty(c => c.DateModifiedUtc).SetElementName("dateModifiedUtc");
            cm.MapProperty(c => c.BaseCurrency).SetElementName("baseCurrency");
            cm.MapProperty(c => c.Currencies).SetElementName("currencies");
        });
    }
}
