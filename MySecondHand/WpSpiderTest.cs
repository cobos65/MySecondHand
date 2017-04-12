using System;
using Xunit;

namespace MySecondHand.Helpers.WebHelper.IntegrationTests
{
    public class WpSpiderTest
    {
        [Fact]
        public void dosearchshouldreturn()
        {

        		var htmlclienthelper= susstitute;
        		var wpspider= new wpspider(htmlclienthelper);
        		var result = htmlclienthelper.GetInnerHtml(url);
        		assert(result!=null);
        }
    }
}
