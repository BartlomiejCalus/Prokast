using Prokast.Server.Entities;
using Prokast.Server.Models.ProductModels;

namespace Prokast.Server.Models.ResponseModels.ProductResponseModels
{
    public class ProductsGetResponse : Response
    {
        public ProductGetDto Model { get; set; }
    }
}
