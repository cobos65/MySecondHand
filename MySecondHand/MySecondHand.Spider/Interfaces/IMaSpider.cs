using HtmlAgilityPack;
using MySecondHand.Spider.Model;
using System.Collections.Generic;

namespace MySecondHand.Spider.Interfaces
{
    public interface IMaSpider:ISpider
    {
        string ComposeSearchUrl(SearchParameter parameter);
    }
}
