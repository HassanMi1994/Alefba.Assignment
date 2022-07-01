using Alefba.Core.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alefba.Test.Mock
{
    public class MockHtmlDownloaderService : IHtmlDownlaoderService
    {

        public async Task<string> LoadHtmlText()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = System.IO.Path.Combine(directory, "Mock", "page.html");
            string htmlText = await File.ReadAllTextAsync(path);
            return htmlText;
        }
    }
}
