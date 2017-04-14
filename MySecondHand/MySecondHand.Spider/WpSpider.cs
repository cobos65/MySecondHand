using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;

namespace MySecondHand.Spider
{
    public class WpSpider
    {
        IHtmlClientHelper _htmlClientHelper;
        string BaseUrl = "www.wallapop.es";

        public WpSpider(IHtmlClientHelper htmlClientHelper)
        {
            _htmlClientHelper = htmlClientHelper;
        }

        public HtmlDocument DoSearch(string parameters)
        {
            var completeurl = ComposeUrl(parameters);

            return _htmlClientHelper.GetInnerHtml(completeurl);
        }

        public string ComposeUrl (string parameters)
        {
            string result = BaseUrl;

            return result;
        }
    }
}
