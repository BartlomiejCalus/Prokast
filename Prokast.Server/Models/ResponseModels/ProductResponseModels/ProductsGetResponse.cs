using Prokast.Server.Entities;
using Prokast.Server.Models.ProductModels;

namespace Prokast.Server.Models.ResponseModels.ProductResponseModels
{
    public class ProductsGetResponse : Response
    {
        public List<Product> Model { get; set; }
    }
}
