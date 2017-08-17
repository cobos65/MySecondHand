using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using MySecondHand.Spider.Model;
using MySecondHand.Spider.Interfaces;

namespace MySecondHand.Spider
{
    public class VbbSpider: IVbbSpider
    {
        public const string BASE_URL = "www.vibbo.com";
        private IHtmlClientHelper _htmlClientHelper;
        private const string THOUSANDS_SEPARATOR = ".";
        private const string DECIMAL_SEPARATOR = "";


        //TODO
        private const string TITTLE_XPATH = ".//*[contains(@class, 'subjectTitle')]";
        private const string CATEGORY_XPATH = "";
        private const string PRICE_XPATH = ".//*[contains(@class, 'subjectPrice')]";
        private const string IMAGE_XPATH = ".//img[contains(@class, 'lazy')]";
        private const string ZONE_XPATH = ".//*[contains(@class, 'zone')]/a";
        private bool _enabled = true;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public SpiderType Type { get => SpiderType.Vbb; }

        public VbbSpider(IHtmlClientHelper htmlClientHelper)
        {
            _htmlClientHelper = htmlClientHelper;
        }

        public HtmlDocument DoSearch(SearchParameter parameter)
        {
            var completeurl = ComposeSearchUrl(parameter);

            return _htmlClientHelper.GetInnerHtml(completeurl);
        }

        public IList<ProductItem> GetSearchedItems(HtmlDocument searchResult)
        {
            IList<ProductItem> items = new List<ProductItem>();

            var findItems = searchResult.DocumentNode
                .Descendants("div")
                .Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Contains("basicList flip-container list_ads_row"));

            foreach (var documentNode in findItems)
            {
                if (IsValidNode(documentNode))
                {
                    var productItem = new ProductItem();
                    productItem.ItemName = documentNode.SelectNodes(TITTLE_XPATH).First().InnerText.Trim();
                    productItem.ItemLink = $"https://{BASE_URL}/{documentNode.SelectNodes(TITTLE_XPATH).First().GetAttributeValue("href", "")}";
                    productItem.ItemPrice = documentNode.SelectNodes(PRICE_XPATH).First().InnerText.Trim().Replace(";", "").Replace("&euro", "€");
                    productItem.ItemZone = documentNode.SelectNodes(ZONE_XPATH).First().InnerText.Trim();
                    productItem.ItemImage = documentNode.SelectNodes(IMAGE_XPATH)
                        .First()
                        .GetAttributeValue("title", "");
                    productItem.ItemHtml = documentNode.InnerHtml;
                    productItem.ItemSource = Type;

                    productItem.ItemPrice = productItem.ItemPrice.Replace(THOUSANDS_SEPARATOR, string.Empty);

                    items.Add(productItem);
                }
            }

            return items;
        }

        public string ComposeSearchUrl(SearchParameter parameter)
        {
            string searchParams = "/anuncios-toda-espana/";
            if (parameter != null)
            {
                if (!string.IsNullOrEmpty(parameter.SearchKey))
                {
                    searchParams = $"/{parameter.SearchKey.Trim().Replace(' ', '-')}.htm";
                }
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
                    && node.SelectNodes(ZONE_XPATH) != null
                    && node.SelectNodes(PRICE_XPATH) != null
                    && node.SelectNodes(IMAGE_XPATH) != null;

                return valid;
            }
        }
    }
}
