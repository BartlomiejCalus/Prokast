using Prokast.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.AccountModels
{
    public class AccountEditDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public int WarehouseID { get; set; }
        [Required]
        public Role Role { get; set; }

    }
}
