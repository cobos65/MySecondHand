using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecondHand.Helpers
{
    public class HtmlClientHelper
    {

        public HtmlClientHelper()
        {
        }

        public HtmlDocument GetInnerHtml(string url)
        {
            HtmlDocument htmlDocument = null;
            using (var client = new HttpClient())
            {            
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = Task.Run(()=>client.GetAsync(url)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();                    
                }
            }

            return htmlDocument;
        }
    }
}
