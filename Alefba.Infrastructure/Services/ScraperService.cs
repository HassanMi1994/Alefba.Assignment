using Alefba.Core.Abstracts;
using Alefba.Core.Enums;
using HtmlAgilityPack;

namespace Alefba.Infrastructure.Services
{
    public class ScraperService : IScraperService
    {
        private readonly HtmlDocument _htmlDocument;
        private readonly IHtmlDownlaoderService _htmlDownlaoderService;

        public ScraperService(IHtmlDownlaoderService htmlDownlaoderService)
        {
            _htmlDocument = new();
            _htmlDownlaoderService = htmlDownlaoderService;
        }

        public async Task<string> ScrapCurrencyRateCell(CurrencyType currencyType, RateTradeType rateTradeType)
        {
            var htmlText = await _htmlDownlaoderService.LoadHtmlText();
            _htmlDocument.LoadHtml(htmlText);
            var row = GetRowContainingCurrencyType(currencyType, _htmlDocument);
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
