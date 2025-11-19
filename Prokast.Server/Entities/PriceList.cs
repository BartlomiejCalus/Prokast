using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class PriceList
    {
        public int ID { get; set; }
        public required string Name { get; set; }

        public int ProductID { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public virtual List<Prices> Prices { get; set; }
    }
}
