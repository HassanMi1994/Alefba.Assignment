using Alefba.Core.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Alefba.Infrastructure.Entities
{
    public class CurrencyHistory : ICurrencyHistory
    {
        [BsonId]
        public Guid ID { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Symbol { get; set; }
        public int Rate { get; set; }

        public override string ToString() => $"{Symbol}: {Rate:N0} Rials [On {Date} - {Time}]";

    }

    [BsonIgnoreExtraElements]
    public class CurrencyHistoryLimited
    {
        public int Rate { get; set; }

    }
}

