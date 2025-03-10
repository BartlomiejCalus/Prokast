using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.StoredProductModels
{
    public class StoredProductGetMinimal
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }

    }
}
