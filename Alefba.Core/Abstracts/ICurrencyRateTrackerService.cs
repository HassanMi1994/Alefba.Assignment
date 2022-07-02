using Alefba.Core.Models;

namespace Alefba.Core.Abstracts
{
    public interface ICurrencyRateTrackerService
    {
        Task<string> GetAverageAsync(DateTime from, DateTime to);
        public Task<ICurrencyHistory> UpdateLastestDollarRateAsync();
    }
}
