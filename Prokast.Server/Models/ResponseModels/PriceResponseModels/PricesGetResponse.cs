using Prokast.Server.Entities;

namespace Prokast.Server.Models.ResponseModels.PriceResponseModels
{
    public class PricesGetResponse : Response
    {
        public List<PriceGetDto> Model { get; set; }
    }

    public class PriceGetDto(Prices price)
    {
        public int ID { get; set; } = price.ID;
        public string Name { get; set; } = price.Name;
        public decimal Netto { get; set; } = price.Netto;
        public decimal VAT { get; set; } = price.VAT;
        public decimal Brutto { get; set; } = price.Brutto;
        public int RegionID { get; set; } = price.RegionID;
    }
}
