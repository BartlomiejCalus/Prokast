using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class AdditionalDescriptionCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Region { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
