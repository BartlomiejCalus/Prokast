using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class Prices
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required decimal Netto { get; set; }
        public required decimal VAT { get; set; }
        public required decimal Brutto { get; set; }

        public int RegionID { get; set; }
        public virtual Region Regions { get; set; }

        public int PriceListID { get; set; }
        [JsonIgnore]
        public virtual PriceList PriceLists { get; set; }
    }
}
