using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Driver;

namespace StocksPortfolio.Infrastructure.Services
{
    public class DataProviderService
    {
        private readonly IMongoCollection<Portfolio> _portfolioCollection;
        private static readonly MongoDbRunner _runner = MongoDbRunner.Start();

        static DataProviderService()
        {
            _runner.Import("portfolioDb", "Portfolios", Path.Combine("Data", "portfolios.json"), true);
        }

        public DataProviderService()
        {
            var client = new MongoClient(_runner.ConnectionString);
            _portfolioCollection = client.GetDatabase("portfolioDb").GetCollection<Portfolio>("Portfolios");
        }

        public async Task<Portfolio> GetPortfolio(ObjectId id)
        {
            var idFilter = Builders<Portfolio>.Filter.Eq(portfolio => portfolio.Id, id);

            return await _portfolioCollection.Find(idFilter).FirstOrDefaultAsync();
        }

        public async Task DeletePortfolio(ObjectId id)
        {
            await _portfolioCollection.DeleteOneAsync(Builders<Portfolio>.Filter.Eq(portfolio => portfolio.Id, id));
        }
    }
}
