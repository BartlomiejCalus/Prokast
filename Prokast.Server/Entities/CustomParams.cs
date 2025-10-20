using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class CustomParams
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Value { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int RegionID { get; set; }
        public virtual Region Regions { get; set; }
    }
}
