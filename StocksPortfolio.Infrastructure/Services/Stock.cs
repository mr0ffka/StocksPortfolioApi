using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Infrastructure.Services
{
    public class Stock
    {
        [BsonElement("ticker")]
        public string Ticker { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }

        [BsonElement("numberOfShares")]
        public int NumberOfShares { get; set; }
    }
}
