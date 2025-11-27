using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.StoredProductModels
{
    public class StoredProductGetDto
    {
        public int ID { get; set; }
        [Required]
        public int WarehouseID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int MinQuantity { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Sku { get; set; }
    }
}
