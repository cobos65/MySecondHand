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
            _wpSpider.Enabled = true;
            _vbbSpider = vbbSpider;
            _vbbSpider.Enabled = true;
            _maSpider = maSpider;
            _maSpider.Enabled = true;
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
                    searchedProducts.AddRange(spiderProducts);
                }
            }
            return searchedProducts;
        }

        public void SetSpiderState(SpiderType spiderType, bool enabled)
        {
            switch (spiderType)
            {
                case SpiderType.Wp:
                    _wpSpider.Enabled = enabled;
                    break;
                case SpiderType.Vbb:
                    _vbbSpider.Enabled = enabled;
                    break;
                case SpiderType.Ma:
                    _maSpider.Enabled = enabled;
                    break;
                default:
                    break;
            }
        }
    }
}
