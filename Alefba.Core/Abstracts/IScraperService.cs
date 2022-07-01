using Alefba.Core.Enums;

namespace Alefba.Core.Abstracts
{
    public interface IScraperService
    {
        public Task<string> ScrapCurrencyRateCell(CurrencyType currencyType, RateTradeType priceType);
    }
}
