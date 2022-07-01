using Alefba.Core.Abstracts;
using Alefba.Core.Enums;
using Alefba.Core.Models;
using Alefba.Infrastructure.Entities;

namespace Alefba.Infrastructure.Services
{
    public class CurrencyRateTrackerService : ICurrencyRateTrackerService
    {
        private readonly IScraperService _tableScraperService;
        private readonly IPriceTrackerRepository _priceTrackerRepository;

        public CurrencyRateTrackerService(IScraperService tableScraperService, IPriceTrackerRepository priceTrackerRepository)
        {
            _tableScraperService = tableScraperService;
            _priceTrackerRepository = priceTrackerRepository;
        }

        public async Task<ICurrencyHistory> UpdateLastestDollarRateAsync()
        {
            var price = await _tableScraperService.ScrapCurrencyRateCell(CurrencyType.USD, RateTradeType.Buy);
            var model = MakeCurrencyHistory(price);
            await _priceTrackerRepository.InsertRecordAsync(model);
            return model;
        }

        private static ICurrencyHistory MakeCurrencyHistory(string price)
        {
            return new CurrencyHistory
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                Rate = int.Parse(price.Replace(",", "")),
                Symbol = CurrencyType.USD.ToString()
            };
        }

        public async Task<double> GetAverageAsync(DateTime from, DateTime to)
        {
            return await _priceTrackerRepository.GetAverageAsync(from, to);
        }
    }
}
