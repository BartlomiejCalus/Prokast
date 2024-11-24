using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Regions
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}