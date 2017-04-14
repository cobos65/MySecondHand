using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using MySecondHand.Spider.Model;

namespace MySecondHand.Spider
{
    public class WpSpider
    {
        public string BaseUrl = "www.wallapop.es";
        private IHtmlClientHelper _htmlClientHelper;

        private const string TITTLE_XPATH = ".//*[contains(@class, 'product-info-title')]";
        private const string CATEGORY_XPATH = ".//*[contains(@class, 'product-info-category')]";
        private const string PRICE_XPATH = ".//*[contains(@class, 'product-info-price')]";
        private const string IMAGE_XPATH = ".//img[contains(@class, 'card-product-image')]";


        public WpSpider(IHtmlClientHelper htmlClientHelper)
        {
            _htmlClientHelper = htmlClientHelper;
        }

        public HtmlDocument DoSearch(string parameters)
        {
            var completeurl = ComposeUrl(parameters);

            return _htmlClientHelper.GetInnerHtml(completeurl);
        }

        public string ComposeUrl(string parameters)
        {
            string result = BaseUrl;

            return result;
        }

        public IList<ProductItem> GetSearchedItems(HtmlDocument searchResult)
        {
            IList<ProductItem> items = new List<ProductItem>();

            var findclasses = searchResult.DocumentNode
                .Descendants("div")
                .Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Contains("card ") && d.Attributes["class"].Value.Contains("card-product ")
                );

            foreach (var documentNode in findclasses)
            {
                if (IsValidNode(documentNode))
                {
                    var productItem = new ProductItem();
                    productItem.ItemName = documentNode.SelectNodes(TITTLE_XPATH).First().InnerHtml;
                    productItem.ItemLink = documentNode.SelectNodes(TITTLE_XPATH).First().GetAttributeValue("href", "");
                    productItem.ItemCategory = documentNode.SelectNodes(CATEGORY_XPATH).First().InnerHtml;
                    productItem.ItemCategoryLink = documentNode.SelectNodes(CATEGORY_XPATH).First().GetAttributeValue("href", "");
                    productItem.ItemPrice = documentNode.SelectNodes(PRICE_XPATH).First().InnerHtml;
                    productItem.ItemImage = documentNode.SelectNodes(IMAGE_XPATH)
                        .First()
                        .GetAttributeValue("src", "");
                    productItem.ItemHtml = documentNode.InnerHtml;

                    items.Add(productItem);
                }
            }

            return items;
        }

        private bool IsValidNode(HtmlNode node)
        {
            bool valid = false;

            if (node == null)
            {
                return valid;
            }
            else
            {
                valid = node.SelectNodes(TITTLE_XPATH) != null 
                    && node.SelectNodes(CATEGORY_XPATH) != null 
                    && node.SelectNodes(PRICE_XPATH) != null 
                    && node.SelectNodes(IMAGE_XPATH) != null;

                return valid;
            }
        }
    }
}
