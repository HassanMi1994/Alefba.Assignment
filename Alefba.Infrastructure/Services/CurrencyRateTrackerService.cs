using Alefba.Core.Abstracts;
using Alefba.Core.Enums;
using Alefba.Core.Models;
using Alefba.Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace Alefba.Infrastructure.Services
{
    public class CurrencyRateTrackerService : ICurrencyRateTrackerService
    {
        private readonly IScraperService _tableScraperService;
        private readonly IPriceTrackerRepository _priceTrackerRepository;
        private readonly ILogger<CurrencyRateTrackerService> _logger;

        public CurrencyRateTrackerService(IScraperService tableScraperService, IPriceTrackerRepository priceTrackerRepository, ILogger<CurrencyRateTrackerService> logger)
        {
            _tableScraperService = tableScraperService;
            _priceTrackerRepository = priceTrackerRepository;
            _logger = logger;
        }

        public async Task<ICurrencyHistory> UpdateLastestDollarRateAsync()
        {
            var price = await _tableScraperService.ScrapCurrencyRateCell(CurrencyType.USD, RateTradeType.Buy);
            var model = MakeCurrencyHistory(price);
            await InsertIfPriceIsLargerThanZero(model);
            return model;
        }

        private async Task InsertIfPriceIsLargerThanZero(ICurrencyHistory model)
        {
            if (model.IsPriceLargerThanZero)
            {
                await _priceTrackerRepository.InsertRecordAsync(model);
            }
            else
            {
                _logger.LogWarning("this price (0) won't be saved in MongoDB because the price is not valid!");
            }
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
