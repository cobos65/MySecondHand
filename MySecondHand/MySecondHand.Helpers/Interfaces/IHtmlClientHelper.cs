using HtmlAgilityPack;

namespace MySecondHand.Helpers.Interfaces
{
    public interface IHtmlClientHelper
    {
        HtmlDocument GetInnerHtml(string url);
    }
}
