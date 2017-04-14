using SecondHand.Helpers;
using Xunit;

namespace MySecondHand.Helpers.WebHelper.IntegrationTests
{
    public class HtmlClientHelperTest
    {
        [Fact]
        public void GetInnerHtmlShouldReturn()
        {
            var url = "www.google.es";
            var htmlClientHelper = new HtmlClientHelper();

            var result = htmlClientHelper.GetInnerHtml(url);
            Assert.NotNull(result);
        }
    }
}
