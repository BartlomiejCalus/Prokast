using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.WarehouseModels
{
    public class WarehouseCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must have format XX-XXX.")]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [RegularExpression(@"\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone Number must have format: 123-456-789.")]
        public string PhoneNumber { get; set; }
    }
}
