using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using MySecondHand.Spider.Model;
using MySecondHand.Spider.Interfaces;
using System;

namespace MySecondHand.Spider
{
    public class MaSpider : IMaSpider
    {
        public const string BASE_URL = "www.milanuncios.com";
        private IHtmlClientHelper _htmlClientHelper;

        private const string TITTLE_XPATH = ".//*[contains(@class, 'aditem-detail-title')]";
        private const string CATEGORY_XPATH = "";
        private const string PRICE_XPATH = ".//*[contains(@class, 'aditem-price')]";
        private const string IMAGE_XPATH = @".//img[contains(@class, 'ef')]|.//img[contains(@class, 'ee')]";
        private const string ZONE_XPATH = ".//*[contains(@class, 'x4 display-desktop')]";
        private bool _enabled = true;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public SpiderType Type { get => SpiderType.Ma; }

        public MaSpider(IHtmlClientHelper htmlClientHelper)
        {
            _htmlClientHelper = htmlClientHelper;
        }

        public HtmlDocument DoSearch(SearchParameter parameter)
        {
            var completeurl = ComposeSearchUrl(parameter);

            return _htmlClientHelper.GetInnerHtml(completeurl);
        }

        public string ComposeSearchUrl(SearchParameter parameter)
        {
            string searchParams = "/anuncios/";
            if (parameter!=null)
            {
                if (!string.IsNullOrEmpty(parameter.SearchKey))
                {
                    searchParams = $"{searchParams}{parameter.SearchKey}.htm";
                }
            }

            searchParams = $"{searchParams}?demanda=n";

            return string.Concat(BASE_URL, searchParams);
        }

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
                    productItem.ItemLink = $"https://{BASE_URL}/{documentNode.SelectNodes(TITTLE_XPATH).First().GetAttributeValue("href", "")}";
                    productItem.ItemCategory = string.IsNullOrEmpty(CATEGORY_XPATH) ? null : documentNode.SelectNodes(CATEGORY_XPATH).First().InnerHtml.Trim();
                    productItem.ItemCategoryLink = string.IsNullOrEmpty(CATEGORY_XPATH) ? null : $"https://{BASE_URL}/{documentNode.SelectNodes(CATEGORY_XPATH).First().GetAttributeValue("href", "")}";
                    productItem.ItemPrice = documentNode.SelectNodes(PRICE_XPATH).First().InnerText.Trim().Replace(";", "").Replace("&euro", "€");
                    productItem.ItemZone = documentNode.SelectNodes(ZONE_XPATH).First().InnerText.Split('(', ')')[1];
                    productItem.ItemImage = documentNode.SelectNodes(IMAGE_XPATH) == null ? null : documentNode.SelectNodes(IMAGE_XPATH)
                        .First()
                        .GetAttributeValue("src", "");
                    productItem.ItemHtml = documentNode.InnerHtml;

                    productItem.ItemSource = Type;
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
                    && node.SelectNodes(ZONE_XPATH) != null
                    && node.SelectNodes(PRICE_XPATH) != null;

                return valid;
            }
        }
    }
}
