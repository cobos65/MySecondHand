using HtmlAgilityPack;
using MySecondHand.Helpers.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Helpers
{
    public class HtmlClientHelper : IHtmlClientHelper
    {

        public HtmlClientHelper()
        {
        }

        public HtmlDocument GetInnerHtml(string url)
        {
            HtmlDocument htmlDocument = null;
            using (var client = new HttpClient())
            {                                
                client.DefaultRequestHeaders.Add("User-Agent","test");
                client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("ISO-8859-15"));
                var response = Task.Run(() => client.GetAsync($"https://{url}")).Result;
                if (response.IsSuccessStatusCode)
                {

                    var responseByteArray =  response.Content.ReadAsByteArrayAsync().Result;
                    htmlDocument = new HtmlDocument();
                    var responseString = Encoding.UTF8.GetString(responseByteArray, 0, responseByteArray.Length - 1);
                    htmlDocument.LoadHtml(responseString);
                }
            }

            return htmlDocument;
        }
    }
}
