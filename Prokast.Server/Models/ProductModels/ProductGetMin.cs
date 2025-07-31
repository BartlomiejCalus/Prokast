using Prokast.Server.Models.PriceModels.PriceListModels;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductGetMin
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string? Photo {  get; set; }
        public DateTime AdditionDate { get; set; }
    }
}
