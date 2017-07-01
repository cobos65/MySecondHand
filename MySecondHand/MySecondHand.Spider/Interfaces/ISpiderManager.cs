using System.Collections.Generic;
using MySecondHand.Spider.Interfaces;
using MySecondHand.Spider.Model;

namespace MySecondHand.Spider.Interfaces
{
    public interface ISpiderManager
    {
        List<ISpider> Spiders { get; set; }

        IList<ProductItem> SearchItems(SearchParameter parameter);

        void SetSpiderState(SpiderType spiderType, bool enabled);
    }
}