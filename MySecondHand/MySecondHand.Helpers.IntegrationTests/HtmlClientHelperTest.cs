using SecondHand.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MySecondHand.Helpers.WebHelper.IntegrationTests
{
    public class HtmlClientHelperTest
    {
        [Fact]
        public void GetInnerHtmlShouldReturn()
        {
            var url = "http://www.google.es";
            var htmlClientHelper = new HtmlClientHelper();

            var result = htmlClientHelper.GetInnerHtml(url);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetInnerHtmlShouldReturn2()
        {
            var url = "https://www.vibbo.com/";
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri($"https://{url}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.CancelPendingRequests();
                var response = Task.Run(() => client.GetAsync(url)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
