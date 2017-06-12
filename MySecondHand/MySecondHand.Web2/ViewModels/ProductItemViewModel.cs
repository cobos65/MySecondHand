using MySecondHand.Spider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySecondHand.Web2.ViewModels
{
    public class ProductItemViewModel
    {
        public string ItemName { get; set; }
        public SpiderType ItemSource { get; set; }
        public string ItemPrice { get; set; }
        public string ItemImage { get; set; }
    }
}
