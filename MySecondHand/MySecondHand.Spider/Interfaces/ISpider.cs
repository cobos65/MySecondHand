using HtmlAgilityPack;
using MySecondHand.Spider.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySecondHand.Spider.Interfaces
{
    public interface ISpider
    {

        bool Enabled { get; set; }

        SpiderType Type { get; }

        HtmlDocument DoSearch(SearchParameter parameter);

        IList<ProductItem> GetSearchedItems(HtmlDocument searchResult);
    }
}
