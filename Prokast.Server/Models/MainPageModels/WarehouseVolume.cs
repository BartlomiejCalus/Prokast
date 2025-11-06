namespace Prokast.Server.Models.MainPageModels
{
    public class WarehouseVolume
    {
        public string WarehouseName { get; set; }
        public int StoredProductsCount { get; set; }
        public List<StoredProductCount> StoredProducts { get; set; }
    }
}
