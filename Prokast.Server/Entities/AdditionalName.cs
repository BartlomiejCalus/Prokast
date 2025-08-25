using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class AdditionalName
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public required string Value { get; set; }

        public required int ProductID {get; set;}
        public virtual Product Product { get; set; }

        public required int RegionID { get; set; }
        public virtual Region Regions { get; set; }
    }
}
