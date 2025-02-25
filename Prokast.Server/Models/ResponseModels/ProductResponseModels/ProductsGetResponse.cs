using Prokast.Server.Models.ProductModels;

namespace Prokast.Server.Models.ResponseModels.ProductResponseModels
{
    public class ProductsGetResponse : Response
    {
        public List<ProductGet> Model { get; set; }
    }
}
