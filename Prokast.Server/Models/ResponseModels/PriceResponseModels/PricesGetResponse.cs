using Prokast.Server.Entities;

namespace Prokast.Server.Models.ResponseModels.PriceResponseModels
{
    public class PricesGetResponse : Response
    {
        public List<Prices> Model { get; set; }
    }
}
