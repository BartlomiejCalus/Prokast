using Prokast.Server.Models.StoredProductModels;

namespace Prokast.Server.Models.ResponseModels.StoredProductResponseModels
{
    public class StoredProductGetMinimalResponse: Response
    {
        public List<StoredProductGetMinimal> Model { get; set; }
    }
}
