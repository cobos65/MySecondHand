using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using NSubstitute;
using System;
using Xunit;
using System.IO;
using System.Reflection;

namespace MySecondHand.Spider.Unit.Tests
{
    public class VbbSpiderTest
    {
        private IHtmlClientHelper _htmlClientHelper;
        private VbbSpider _vbbSpider;
        private HtmlDocument _mockHtmlDocument;
        private const string BASE_PATH = @"D:\Projects\Git\MySecondHand\MySecondHand.Spider.Unit.Tests\";

        
        public VbbSpiderTest()
        {
            _htmlClientHelper = Substitute.For<IHtmlClientHelper>();
            _vbbSpider = new VbbSpider(_htmlClientHelper);

            _mockHtmlDocument = new HtmlDocument();
            var htmlFile = File.ReadAllText(BASE_PATH + @"HtmlExamples\view-source_www.vibbo.com_anuncios-toda-espana_.html");
            _mockHtmlDocument.LoadHtml(htmlFile);
        }

        [Fact]                
        public void DoSearchShouldReturn()
        {
            _htmlClientHelper.GetInnerHtml(Arg.Any<string>()).Returns(_mockHtmlDocument);
            
            var result = _vbbSpider.DoSearch(null);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetSearchedItemsWithValidHtmlDocumentShouldReturn()
        {
            var result = _vbbSpider.GetSearchedItems(_mockHtmlDocument);

            Assert.True(result.Count > 0);
            Assert.NotNull(result[0]);
            Assert.Equal(result[0].ItemName, "PS4");
            Assert.Equal(result[0].ItemPrice, "200€");
            Assert.Null(result[0].ItemCategory);
            Assert.Equal(result[0].ItemZone, "Asturias");

        }

        [Fact]
        public void ComposeSearchUrlWithValidParamsShouldReturnValidUrl()
        {
            //TODO
        }

        [Fact]
        public void ComposeSearchUrlWithNullParamsShouldReturnBaseUrl()
        {
            //TODO
        }
    }
}
