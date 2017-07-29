using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using MySecondHand.Spider.Model;
using MySecondHand.Spider.Interfaces;
using System;

namespace MySecondHand.Spider
{
    public class WpSpider : IWpSpider
    {

        private const int RETRIES = 3;
        private IHtmlClientHelper _htmlClientHelper;

        private const string TITTLE_XPATH = ".//*[contains(@class, 'product-info-title')]";
        private const string CATEGORY_XPATH = ".//*[contains(@class, 'product-info-category')]";
        private const string PRICE_XPATH = ".//*[contains(@class, 'product-info-price')]";
        private const string IMAGE_XPATH = ".//img[contains(@class, 'card-product-image')]";
        private bool _enabled = true;

        public const string BASE_URL = "es.wallapop.com";

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public SpiderType Type { get => SpiderType.Wp; }

        public WpSpider(IHtmlClientHelper htmlClientHelper)
        {
            _htmlClientHelper = htmlClientHelper;
        }

        public HtmlDocument DoSearch(SearchParameter parameter)
        {
            HtmlDocument htmlDocument = null;
            var completeurl = ComposeSearchUrl(parameter);
            bool emptyDocument = true;

            for (int i = 0; i < RETRIES && emptyDocument; i++)
            {
                htmlDocument = _htmlClientHelper.GetInnerHtml(completeurl);
                emptyDocument = string.IsNullOrEmpty(htmlDocument.DocumentNode.InnerHtml);
            }

            return htmlDocument;
        }

        public IList<ProductItem> GetSearchedItems(HtmlDocument searchResult)
        {
            IList<ProductItem> items = new List<ProductItem>();

            var findItems = searchResult.DocumentNode
                .Descendants("div")
                .Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Contains("card ") && d.Attributes["class"].Value.Contains("card-product ")
                );

            foreach (var documentNode in findItems)
            {
                if (IsValidNode(documentNode))
                {
                    var productItem = new ProductItem();
                    productItem.ItemName = documentNode.SelectNodes(TITTLE_XPATH).First().InnerHtml.Trim();
                    productItem.ItemLink = $"https://{BASE_URL}/{documentNode.SelectNodes(TITTLE_XPATH).First().GetAttributeValue("href", "")}";
                    productItem.ItemCategory = documentNode.SelectNodes(CATEGORY_XPATH).First().InnerHtml.Trim();
                    productItem.ItemCategoryLink = $"https://{BASE_URL}/{documentNode.SelectNodes(CATEGORY_XPATH).First().GetAttributeValue("href", "")}";
                    productItem.ItemPrice = documentNode.SelectNodes(PRICE_XPATH).First().InnerHtml.Trim();
                    productItem.ItemImage = documentNode.SelectNodes(IMAGE_XPATH)
                        .First()
                        .GetAttributeValue("src", "");
                    productItem.ItemHtml = documentNode.InnerHtml;
                    productItem.ItemSource = Type;

                    items.Add(productItem);
                }
            }

            return items;
        }

        public string ComposeSearchUrl(SearchParameter parameter)
        {
            string searchParams = string.Empty;

            if (parameter != null)
            {
                searchParams = $"/search?kws={parameter.SearchKey}&lat={parameter.Latitude}&lng={parameter.Longitude}";
            }
            return string.Concat(BASE_URL, searchParams);
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
