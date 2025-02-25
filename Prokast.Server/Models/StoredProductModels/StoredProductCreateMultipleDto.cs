using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.StoredProductModels
{
    public class StoredProductCreateMultipleDto
    {
        [Required]
        public List<StoredProductCreateDto> StoredProducts { get; set; }
    }
}
