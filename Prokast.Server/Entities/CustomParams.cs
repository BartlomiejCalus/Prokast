using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class CustomParams
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Value { get; set; }

        public int ProductID { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public int RegionID { get; set; }
        public virtual Region Regions { get; set; }
    }
}
