using Alefba.Core.Abstracts;
using Alefba.Core.Models;
using Alefba.Infrastructure.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Alefba.Infrastructure.Repository
{
    public class PriceTrackerRepository : IPriceTrackerRepository
    {
        private const string TABLE_NAME = "DollarHistory";
        private IMongoDatabase _db;

        public PriceTrackerRepository()
        {
            var client = new MongoClient();
            _db = client.GetDatabase("CurrencyRateHistory");

        }

        public async Task InsertRecordAsync(ICurrencyHistory currencyHistory)
        {
            var collection = _db.GetCollection<CurrencyHistory>(TABLE_NAME);
            await collection.InsertOneAsync((CurrencyHistory)currencyHistory);
        }

        public async Task<List<ICurrencyHistory>> GetAllCurrencyHistory()
        {
            var collection = _db.GetCollection<CurrencyHistory>(TABLE_NAME);
            var jsonCollection = await collection.FindAsync(new BsonDocument());
            return jsonCollection.ToList<ICurrencyHistory>();
        }

        public async Task<List<ICurrencyHistory>> GetAllCurrencyHistoryByDate(DateTime from, DateTime to)
        {
            var collection = _db.GetCollection<CurrencyHistory>(TABLE_NAME);
            var jsonCollection = await collection.FindAsync(x => x.Date >= from && x.Date <= to);
            return jsonCollection.ToList<ICurrencyHistory>();
        }

        public async Task<double> GetAverageAsync(DateTime from, DateTime to)
        {
            var all = await GetAllCurrencyHistoryByDate(from, to);
            return all.Average(x => x.Rate);
        }
    }
}
