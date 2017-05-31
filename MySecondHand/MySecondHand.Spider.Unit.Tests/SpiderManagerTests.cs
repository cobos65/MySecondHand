using HtmlAgilityPack;
using MySecondHand.Spider.Interfaces;
using MySecondHand.Spider.Model;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MySecondHand.Spider.Unit.Tests
{
    public class SpiderManagerTests
    {
        private List<ProductItem> mockProductItems;

        private List<ProductItem> _mockWpProducts;
        private List<ProductItem> _mockVbbProducts;

        private IWpSpider _wpSpider;
        private IVbbSpider _vbbSpider;
        private IMaSpider _maSpider;
        private ISpiderManager _spiderManager;

        public SpiderManagerTests()
        {
            _wpSpider = Substitute.For<IWpSpider>();
            _vbbSpider = Substitute.For<IVbbSpider>();
            _maSpider = Substitute.For<IMaSpider>();
            _wpSpider.Enabled = true;
            _vbbSpider.Enabled = true;
            _maSpider.Enabled = true;

            _spiderManager = new SpiderManager(_wpSpider, _vbbSpider, _maSpider);

            var mockProduct1 = new ProductItem()
            {
                ItemCategory = "category1",
                ItemCategoryLink = "categoryLink1",
                ItemHtml = "html",
                ItemImage = "image1",
                ItemLink = "link",
                ItemName = "item1",
                ItemPrice = "20€",
                ItemZone = "zone1"
            };
            var mockProduct2 = new ProductItem()
            {
                ItemCategory = "category2",
                ItemCategoryLink = "categoryLink2",
                ItemHtml = "htm2",
                ItemImage = "image2",
                ItemLink = "link",
                ItemName = "item2",
                ItemPrice = "30€",
                ItemZone = "zone2"
            };
            var mockProduct3 = new ProductItem()
            {
                ItemCategory = "category3",
                ItemCategoryLink = "categoryLink3",
                ItemHtml = "html",
                ItemImage = "image3",
                ItemLink = "link",
                ItemName = "item3",
                ItemPrice = "50€",
                ItemZone = "zone3"
            };

            _mockWpProducts = new List<ProductItem>()
            {
                mockProduct1,
                mockProduct2
            };

            _mockVbbProducts = new List<ProductItem>()
            {
                mockProduct3
            };
        }


        [Fact]
        public void GetSearchedItemsWithValidHtmlDocumentShouldReturn()
        {
            var searchParam = new SearchParameter()
            {
                SearchKey = "key"
            };

            _wpSpider.DoSearch(Arg.Any<SearchParameter>()).Returns(new HtmlDocument());
            _vbbSpider.DoSearch(Arg.Any<SearchParameter>()).Returns(new HtmlDocument());
            _maSpider.DoSearch(Arg.Any<SearchParameter>()).Returns(new HtmlDocument());
            _wpSpider.GetSearchedItems(Arg.Any<HtmlDocument>()).Returns(_mockWpProducts);
            _vbbSpider.GetSearchedItems(Arg.Any<HtmlDocument>()).Returns(_mockVbbProducts);

            var result = _spiderManager.SearchItems(searchParam);

            Assert.True(result.Count == 3);
            Assert.NotNull(result[0]);
            Assert.Equal(result[0].ItemName, "item1");
            Assert.Equal(result[1].ItemName, "item2");
            Assert.Equal(result[2].ItemName, "item3");
            Assert.Equal(result[0].ItemSource, SpiderType.Wp);
            Assert.Equal(result[1].ItemSource, SpiderType.Wp);
            Assert.Equal(result[2].ItemSource, SpiderType.Wp);

        }

    }
}
