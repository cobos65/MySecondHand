using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using MySecondHand.Spider.Model;

namespace MySecondHand.Spider
{
    public class MaSpider
    {
        public const string BASE_URL = "www.milanuncios.es";
        private IHtmlClientHelper _htmlClientHelper;

        //TODO
        private const string TITTLE_XPATH = ".//*[contains(@class, 'aditem-detail-title')]";
        private const string CATEGORY_XPATH = "";
        private const string PRICE_XPATH = ".//*[contains(@class, 'aditem-price')]";
        private const string IMAGE_XPATH = ".//img[contains(@class, 'ef')]";
        private const string ZONE_XPATH = ".//*[contains(@class, 'x4 display-desktop')]";



        public MaSpider(IHtmlClientHelper htmlClientHelper)
        {
            _htmlClientHelper = htmlClientHelper;
        }

        public HtmlDocument DoSearch(SearchParameter parameter)
        {
            var completeurl = ComposeSearchUrl(parameter);

            return _htmlClientHelper.GetInnerHtml(completeurl);
        }

        //TODO
        public string ComposeSearchUrl(SearchParameter parameter)
        {
            string searchParams = string.Empty;

            return string.Concat(BASE_URL, searchParams);
        }

        //TODO
        public IList<ProductItem> GetSearchedItems(HtmlDocument searchResult)
        {
            IList<ProductItem> items = new List<ProductItem>();

            var findItems = searchResult.DocumentNode
                .Descendants("div")
                .Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Equals("aditem"));

            foreach (var documentNode in findItems)
            {
                if (IsValidNode(documentNode))
                {
                    var productItem = new ProductItem();
                    productItem.ItemName = documentNode.SelectNodes(TITTLE_XPATH).First().InnerHtml.Trim();
                    productItem.ItemLink = documentNode.SelectNodes(TITTLE_XPATH).First().GetAttributeValue("href", "");
                    productItem.ItemCategory = string.IsNullOrEmpty(CATEGORY_XPATH) ? null : documentNode.SelectNodes(CATEGORY_XPATH).First().InnerHtml.Trim();
                    productItem.ItemCategoryLink = string.IsNullOrEmpty(CATEGORY_XPATH) ? null : documentNode.SelectNodes(CATEGORY_XPATH).First().GetAttributeValue("href", "");
                    productItem.ItemPrice = documentNode.SelectNodes(PRICE_XPATH).First().InnerText.Trim().Replace(";", "").Replace("&euro", "€");
                    productItem.ItemZone = documentNode.SelectNodes(ZONE_XPATH).First().InnerText.Split('(', ')')[1];
                    productItem.ItemImage = documentNode.SelectNodes(IMAGE_XPATH) == null ? null : documentNode.SelectNodes(IMAGE_XPATH)
                        .First()
                        .GetAttributeValue("src", "");
                    productItem.ItemHtml = documentNode.InnerHtml;

                    items.Add(productItem);
                }
            }

            return items;
        }

        //TODO
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
                    && node.SelectNodes(ZONE_XPATH) != null
                    && node.SelectNodes(PRICE_XPATH) != null;

                return valid;
            }
        }
    }
}
