using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.AccountModels
{
    public class AccountEditPasswordDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
