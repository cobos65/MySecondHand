using MySecondHand.Spider.Interfaces;
using MySecondHand.Spider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySecondHand.Spider
{
    public class SpiderManager : ISpiderManager
    {
        public List<ISpider> Spiders { get; set; }

        private IWpSpider _wpSpider;
        private IVbbSpider _vbbSpider;
        private IMaSpider _maSpider;


        public SpiderManager(IWpSpider wpSpider, IVbbSpider vbbSpider, IMaSpider maSpider)
        {
            _wpSpider = wpSpider;
            _vbbSpider = vbbSpider;
            _maSpider = maSpider;
            Spiders = new List<ISpider>()
            {
                _wpSpider,
                _vbbSpider,
                _maSpider
            };
        }

        public IList<ProductItem> SearchItems(SearchParameter parameter)
        {
            List<ProductItem> searchedProducts = new List<ProductItem>();

            foreach (var spider in Spiders.Where(spi => spi.Enabled == true))
            {
                var htmlDocument = spider.DoSearch(parameter);
                if (htmlDocument != null)
                {
                    var spiderProducts = spider.GetSearchedItems(htmlDocument);
                    spiderProducts = spiderProducts.Select(product => { product.ItemSource = spider.Type; return product; }).ToList();
                    searchedProducts.AddRange(spiderProducts);
                }
            }
            return searchedProducts;
        }
    }
}
