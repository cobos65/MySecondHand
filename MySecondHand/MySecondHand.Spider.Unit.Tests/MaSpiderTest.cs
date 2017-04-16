using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using NSubstitute;
using System;
using Xunit;
using System.IO;
using System.Reflection;

namespace MySecondHand.Spider.Unit.Tests
{
    public class MaSpiderTest
    {
        private IHtmlClientHelper _htmlClientHelper;
        private MaSpider _maSpider;
        private HtmlDocument _mockHtmlDocument;
        private const string BASE_PATH = @"D:\Projects\Git\MySecondHand\MySecondHand.Spider.Unit.Tests\";

        
        public MaSpiderTest()
        {
            _htmlClientHelper = Substitute.For<IHtmlClientHelper>();
            _maSpider = new MaSpider(_htmlClientHelper);

            _mockHtmlDocument = new HtmlDocument();
            var htmlFile = File.ReadAllText(BASE_PATH + @"HtmlExamples\");
            _mockHtmlDocument.LoadHtml(htmlFile);
        }

        [Fact]                
        public void DoSearchShouldReturn()
        {
            _htmlClientHelper.GetInnerHtml(Arg.Any<string>()).Returns(_mockHtmlDocument);
            
            var result = _maSpider.DoSearch(null);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetSearchedItemsWithValidHtmlDocumentShouldReturn()
        {
            var result = _maSpider.GetSearchedItems(_mockHtmlDocument);

            Assert.True(result.Count > 0);
            Assert.NotNull(result[0]);
            Assert.Equal(result[0].ItemName, "Miniaturas agotadas.");
            Assert.Equal(result[0].ItemPrice, "120€");
            Assert.Equal(result[0].ItemCategory, "Moda y Accesorios");
        }
    }
}
