namespace MySecondHand.Spider.Model
{
    public class ProductItem
    {
        public string ItemName { get; set; }
        public string ItemLink { get; set; }
        public string ItemCategory { get; set; }
        public string ItemCategoryLink { get; set; }
        public string ItemPrice { get; set; }
        public string ItemImage { get; set; }
        public string ItemHtml { get; set; }
        public string ItemZone { get; set; }
        public SpiderType ItemSource { get; set; }
        public Currency ItemValue
        {
            get
            {
                Currency.TryParse(ItemPrice, out Currency currency);
                return currency;
            }
        }
    }
}
