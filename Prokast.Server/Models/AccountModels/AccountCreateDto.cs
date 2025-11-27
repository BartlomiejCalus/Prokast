using Prokast.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.AccountModels
{
    public class AccountCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int? WarehouseID { get; set; }
        public int RoleId { get; set; }
    }
}
