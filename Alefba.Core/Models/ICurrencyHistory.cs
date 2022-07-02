namespace Alefba.Core.Models
{
    public interface ICurrencyHistory
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Symbol { get; set; }
        public int Rate { get; set; }
    }
}
