using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class RegionsDto
    {
        [Required]
        public string Name { get; set; }
        
    }
}