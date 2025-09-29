using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.StoredProductModels
{
    public class StoredProductCreateDto
    {
        //[Required]
        //public int WarehouseID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int MinQuantity { get; set; }
        //[Required]
        //public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
