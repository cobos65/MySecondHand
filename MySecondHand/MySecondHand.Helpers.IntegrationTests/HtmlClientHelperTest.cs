using SecondHand.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MySecondHand.Helpers.WebHelper.IntegrationTests
{
    public class HtmlClientHelperTest
    {
        [Fact]
        public void GetInnerHtmlVibboShouldReturn()
        {
            var url = "www.vibbo.com/";
            var htmlClientHelper = new HtmlClientHelper();

            var result = htmlClientHelper.GetInnerHtml(url);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetInnerHtmlWallapopShouldReturn()
        {
            var url = "www.wallapop.com";
            var htmlClientHelper = new HtmlClientHelper();

            var result = htmlClientHelper.GetInnerHtml(url);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetInnerHtmlSegundamanoShouldReturn()
        {
            var url = "www.milanuncios.com";
            var htmlClientHelper = new HtmlClientHelper();

            var result = htmlClientHelper.GetInnerHtml(url);
            Assert.NotNull(result);
        }
    }
}
