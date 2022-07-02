using Alefba.Core.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Alefba.Infrastructure.Entities
{
    public class CurrencyHistory : ICurrencyHistory
    {
        [BsonId]
        public Guid ID { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Symbol { get; set; }
        public int Rate { get; set; }

        public override string ToString()
        {
            return $"{Symbol}: {Rate:N0} Rial On {Date.ToString("yyyy-MM-dd")}  At  {Time.ToString(@"hh\:mm\:ss")}";
        }
    }
}

