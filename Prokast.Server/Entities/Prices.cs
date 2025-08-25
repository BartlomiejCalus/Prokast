using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Prices
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required decimal Netto { get; set; }
        public required decimal VAT { get; set; }
        public required decimal Brutto { get; set; }

        public required int RegionID { get; set; }
        public virtual Region Regions { get; set; }

        public required int PriceListID { get; set; }
        public virtual PriceList PriceLists { get; set; }
    }
}
