using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.StoredProductModels
{
    public class StoredProductCreateMultipleDto
    {
        [Required]
        public StoredProductCreateDto StoredProducts { get; set; }
    }
}
