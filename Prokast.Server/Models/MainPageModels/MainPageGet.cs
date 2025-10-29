namespace Prokast.Server.Models.MainPageModels
{
    public class MainPageGet
    {
        public int WarehouseCount { get; set; }
        public int StoredProductsCount { get; set; }
        public StoredProductCount TopProduct { get; set; }
        public StoredProductCount LastDelivery { get; set; }
        public List<WarehouseVolume> WarehouseVolumeList { get; set; }
    }
}
