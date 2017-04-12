using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHand.Spider
{
    public class WpSpider
    {
    		IHtmlClientHelper _htmlclientHelper;
    		string BaseUrl ="www.wallapop.es";
    		
        public WpSpider(IHtmlClientHelper htmlclientHelper)
        {
        		_htmlclientHelper=htmlclientHelper;
        }
        
        public HmlDocument DoSearch(string params)
        {
        		var completeurl= composeUrl(params);
        		
        		return _htmlClienthelper.GetInnerHtml(completeurl);
        }
    }
}
