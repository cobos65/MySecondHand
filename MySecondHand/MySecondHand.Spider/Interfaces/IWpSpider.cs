using HtmlAgilityPack;
using MySecondHand.Spider.Model;
using System.Collections.Generic;

namespace MySecondHand.Spider.Interfaces
{
    public interface IWpSpider:ISpider
    {        
        string ComposeSearchUrl(SearchParameter parameter);        
    }
}
