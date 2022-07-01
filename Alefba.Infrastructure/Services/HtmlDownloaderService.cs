using Alefba.Core.Abstracts;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;

namespace Alefba.Infrastructure.Services
{
    public class HtmlDownloaderService : IHtmlDownlaoderService
    {
        private const string WEB_PAGE_URL = @"https://mex.co.ir/";
        private readonly HtmlWeb _htmlWeb;

        public HtmlDownloaderService()
        {
            _htmlWeb = new HtmlWeb();
        }

        public async Task<string> LoadHtmlText()
        {
            var doc = await _htmlWeb.LoadFromWebAsync(WEB_PAGE_URL);
            string htmlText = doc.Text;
            return htmlText;
        }
    }
}
