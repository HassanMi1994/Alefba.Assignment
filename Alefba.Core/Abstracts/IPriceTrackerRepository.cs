using Alefba.Core.Models;

namespace Alefba.Core.Abstracts
{
    public interface IPriceTrackerRepository
    {
        Task<List<ICurrencyHistory>> GetAllCurrencyHistory();
        Task<List<ICurrencyHistory>> GetAllCurrencyHistoryByDate(DateTime from, DateTime to);
        Task<double> GetAverageAsync(DateTime from, DateTime to);
        Task InsertRecordAsync(ICurrencyHistory currencyHistory);
    }
}
