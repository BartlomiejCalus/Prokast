using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string SKU { get; set; }
        public required string EAN { get; set; }
        public required string Description { get; set; }
        public DateTime AdditionDate { get; set; } = DateTime.Now;
        public DateTime ModificationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual List<AdditionalDescription> AdditionalDescriptions { get; set; }
        [JsonIgnore]
        public virtual List<AdditionalName> AdditionalNames { get; set; }
        [JsonIgnore]
        public virtual List<CustomParams> CustomParams { get; set; }
        [JsonIgnore]
        public virtual List<DictionaryParams> DictionaryParams { get; set; }
        [JsonIgnore]
        public virtual List<Photo> Photos { get; set; }

        public virtual PriceList PriceList { get; set; }

        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        //public int? StoredProductID { get; set; }

        public virtual StoredProduct? StoredProduct { get; set; }
        
        [JsonIgnore]
        public virtual List<OrderProduct>? OrderProducts { get; set; } = [];
    }
}
