namespace Prokast.Server.Models.MainPageModels
{
    public class StoredProductCount
    {
        public string StoredProductSKU { get; set; }
        public int StoredProductQuantity { get; set; }
        public bool IsBelowMinimum { get; set; }
    }
}
