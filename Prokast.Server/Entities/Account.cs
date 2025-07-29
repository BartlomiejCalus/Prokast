using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public required string Password { get; set; }
        public int? WarehouseID { get; set; }
        public int? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? ClientID { get; set; }
    }
}
