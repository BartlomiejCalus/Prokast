using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class StoredProduct
    {
        public int ID { get; set; }
        public required int Quantity { get; set; }
        public required int MinQuantity { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public int WarehouseID { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        public int? ProductID { get; set; }
        public virtual Product? Product { get; set; }
    }
}
