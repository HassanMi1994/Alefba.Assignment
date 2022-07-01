using Alefba.Core.Enums;
using Alefba.Infrastructure.Services;
using Alefba.Test.Mock;

namespace Alefba.Test
{
    [TestFixture]
    public class ScraperTest
    {
        MockHtmlDownloaderService _downloaderService;
        ScraperService _scraperService;

        [SetUp]
        public void Setup()
        {
            _downloaderService = new MockHtmlDownloaderService();
            _scraperService = new ScraperService(_downloaderService);
        }

        [Test]
        public async Task ScrapCurrencyRateCell_Does_Not_Return_Empty_String()
        {
            string price = await _scraperService.ScrapCurrencyRateCell(CurrencyType.USD, RateTradeType.Sell);
            Assert.IsNotNull(price, $"{price} should not be null ever.");
        }

        [Test]
        public async Task ScrapCurrencyRateCell_USD_Sell_Price_Should_Be_277276()
        {
            string price = await _scraperService.ScrapCurrencyRateCell(CurrencyType.USD, RateTradeType.Sell);
            Console.WriteLine(price);
            Assert.AreEqual(price, "277,276");
        }

        [Test]
        public async Task ScrapCurrencyRateCell_USD_Buy_Price_Should_Be_274518()
        {
            string price = await _scraperService.ScrapCurrencyRateCell(CurrencyType.USD, RateTradeType.Buy);
            Console.WriteLine(price);
            Assert.AreEqual(price, "274,518");
        }

        [Test]
        public async Task ScrapCurrencyRateCell_Eur_Buy_Price_Should_Be_283682()
        {
            string price = await _scraperService.ScrapCurrencyRateCell(CurrencyType.EUR, RateTradeType.Buy);
            Console.WriteLine(price);
            Assert.AreEqual(price, "283,682");
        }

        [Test]
        public async Task ScrapCurrencyRateCell_Eur_Sell_Price_Should_Be_286534()
        {
            string price = await _scraperService.ScrapCurrencyRateCell(CurrencyType.EUR, RateTradeType.Sell);
            Console.WriteLine(price);
            Assert.AreEqual(price, "286,534");
        }
    }
}