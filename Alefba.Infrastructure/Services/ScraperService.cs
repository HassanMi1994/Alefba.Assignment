using Alefba.Core.Abstracts;
using Alefba.Core.Enums;
using HtmlAgilityPack;

namespace Alefba.Infrastructure.Services
{
    public class ScraperService : IScraperService
    {
        private const string WEB_PAGE_URL = @"https://mex.co.ir/";
        private readonly HtmlWeb _htmlWeb;

        public ScraperService()
        {
            _htmlWeb = new();
        }

        public async Task<string> ScrapCurrencyRateCell(CurrencyType currencyType, RateTradeType rateTradeType)
        {
            var document = await _htmlWeb.LoadFromWebAsync(WEB_PAGE_URL);
            var row = GetRowContainingCurrencyType(currencyType, document);
            return row[(int)rateTradeType];
        }

        private List<string> GetRowContainingCurrencyType(CurrencyType currencyType, HtmlDocument document)
        {
            var query = from table in document.DocumentNode.SelectNodes("//table").Cast<HtmlNode>()
                        from tbody in table.SelectNodes("tbody").Cast<HtmlNode>()
                        from row in tbody.SelectNodes("tr").Cast<HtmlNode>()
                        from cell in row.SelectNodes("td").Cast<HtmlNode>()
                        where cell.InnerText == currencyType.ToString()
                        select row;

            return query.First().SelectNodes("td").Select(x => x.InnerText.Trim()).ToList();
        }
    }
}
