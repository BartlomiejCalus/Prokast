using Prokast.Server.Entities;
using Prokast.Server.Models.StoredProductModels;

namespace Prokast.Server.Models.ResponseModels.StoredProductResponseModels
{
    public class StoredProductGetResponse : Response
    {
        public List<StoredProductGetDto> Model { get; set; }
    }
}
