using Prokast.Server.Models.PriceModels;

namespace Prokast.Server.Models.ResponseModels.PriceResponseModels
{
    public class PricesEditResponse : Response
    {
        public EditPriceDto Model { get; set; }
    }
}
