using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Prokast.Server.Entities
{
    public class Buyer
    {
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        [RegularExpression(@"\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone Number must have format: 123-456-789.")]
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string? HouseNumber {get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must have format XX-XXX.")]
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        [RegularExpression(@"^[A-Z]{2}-\d{3}-\d{6}-\d$", ErrorMessage = "NIP must have format XX-123-456789-5.")]
        public string? NIP { get; set; }

        [JsonIgnore]
        public virtual List<Order> Orders { get; set; }
    }
}
