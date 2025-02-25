using Prokast.Server.Models.PriceModels.PriceListModels;

namespace Prokast.Server.Models.ResponseModels.PriceResponseModels.PriceListResponseModels
{
    public class PriceListsGetResponse : Response
    {
        public List<PriceListGet> Model { get; set; }
    }
}
