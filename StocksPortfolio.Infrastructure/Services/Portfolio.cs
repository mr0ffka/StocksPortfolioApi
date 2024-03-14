using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Infrastructure.Services
{
    public class Portfolio
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("totalValue")]
        public float CurrentTotalValue { get; set; }

        [BsonElement("stocks")]
        public ICollection<Stock> Stocks { get; set; }
    }
}
