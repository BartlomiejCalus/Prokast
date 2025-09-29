using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace Prokast.Server.Entities
{
    public class Account
    {
        public int ID { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public int? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public int? ClientID { get; set; }
        public virtual Client Client { get; set; }

        public int? WarehouseID { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
