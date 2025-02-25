using Prokast.Server.Entities;

namespace Prokast.Server.Models.ResponseModels.StoredProductResponseModels
{
    public class StoredProductGetResponse : Response
    {
        public List<StoredProduct> Model { get; set; }
    }
}
