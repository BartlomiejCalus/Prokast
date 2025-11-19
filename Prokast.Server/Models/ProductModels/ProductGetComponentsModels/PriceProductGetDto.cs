namespace Prokast.Server.Models.ProductModels.ProductGetComponentsModels
{
    public class PriceProductGetDto
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required decimal Netto { get; set; }
        public required decimal VAT { get; set; }
        public required decimal Brutto { get; set; }
        public required int RegionID { get; set; }

    }
}
