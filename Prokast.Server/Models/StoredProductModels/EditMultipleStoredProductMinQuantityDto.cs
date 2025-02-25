using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.StoredProductModels
{
    public class EditMultipleStoredProductMinQuantityDto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int MinQuantity { get; set; }
    }

}
