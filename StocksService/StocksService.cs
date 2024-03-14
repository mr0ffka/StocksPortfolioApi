namespace StocksService
{
    public interface IStocksService
    {
        Task<StockModel> GetStockPrice(string ticker);
    }

    public class StocksService : IStocksService
    {
        private readonly string[] _currencies = ["PLN", "EUR", "USD", "JPY", "GBP"];

        public async Task<StockModel> GetStockPrice(string ticker)
        {
            var random = new Random();

            return await Task.FromResult(new StockModel
            {
                Price = random.Next(10, 1000),
                Currency = _currencies[random.Next(0, _currencies.Length)],
            });
        }
    }

    public class StockModel
    {
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
