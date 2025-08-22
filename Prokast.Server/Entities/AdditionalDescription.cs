using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prokast.Server.Entities
{
    public class AdditionalDescription
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public required string Value { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int RegionID { get; set; }
        
        public virtual Region Regions { get; set; }
    }
}
