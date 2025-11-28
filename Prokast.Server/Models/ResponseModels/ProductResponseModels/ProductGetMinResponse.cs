using Prokast.Server.Models.ProductModels;

namespace Prokast.Server.Models.ResponseModels.ProductResponseModels
{
    public class ProductGetMinResponse: Response
    {
        public List<ProductGetMin> Model { get; set; }
        public int TotalItems { get; set; }
    }
}
