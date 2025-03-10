using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.WarehouseModels
{
    public class WarehouseGetMinimal
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
