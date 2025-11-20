using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.PhotoModels
{
    public class PhotoGetDto
    {
        [Required]
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}
