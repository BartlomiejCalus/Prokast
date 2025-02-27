using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone Number must have format: 123-456-789.")]
        public string PhoneNumber { get; set; }
    }
}
