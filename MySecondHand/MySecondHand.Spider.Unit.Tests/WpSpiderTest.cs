using MySecondHand.Helpers.Interfaces;
using HtmlAgilityPack;
using NSubstitute;
using System;
using Xunit;

namespace MySecondHand.Spider.Unit.Tests
{
    public class WpSpiderTest
    {
        private IHtmlClientHelper _htmlClientHelper;

        [Fact]
        public void DoSearchShouldReturn()
        {
            var mockHtmlDocument = new HtmlDocument();
            
            _htmlClientHelper = Substitute.For<IHtmlClientHelper>();

            _htmlClientHelper.GetInnerHtml(Arg.Any<string>()).Returns(mockHtmlDocument);

            var wpSpider = new WpSpider(_htmlClientHelper);

            var result = wpSpider.DoSearch(null);

            Assert.NotNull(result);


        }
    }
}
