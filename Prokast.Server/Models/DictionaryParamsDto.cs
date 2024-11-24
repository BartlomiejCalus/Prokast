using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class DictionaryParamsDto
    {
        [Required]
        public int IdRegion { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Value { get; set; }
    }
}