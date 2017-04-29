﻿using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using MySecondHand.Spider.Model;

namespace MySecondHand.Spider
{
    public class MaSpider
    {
        public const string BASE_URL = "www.wallapop.es";
        private IHtmlClientHelper _htmlClientHelper;

        //TODO
        private const string TITTLE_XPATH = ".//*[contains(@class, '')]";
        private const string CATEGORY_XPATH = "";
        private const string PRICE_XPATH = ".//*[contains(@class, '')]";
        private const string IMAGE_XPATH = ".//img[contains(@class, '')]";
        private const string ZONE_XPATH = ".//*[contains(@class, '')]";
        


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
                .Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Contains("basicList flip-container list_ads_row"));

            foreach (var documentNode in findItems)
            {
                var productItem = new ProductItem();
                productItem.ItemName = documentNode.SelectNodes(TITTLE_XPATH).First().InnerHtml;
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
                    && node.SelectNodes(PRICE_XPATH) != null
                    && node.SelectNodes(IMAGE_XPATH) != null;

                return valid;
            }
        }
    }
}