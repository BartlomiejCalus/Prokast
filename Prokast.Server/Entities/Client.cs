using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class Client
    {
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string BusinessName { get; set; }
        [RegularExpression(@"^[A-Z]{2}-\d{3}-\d{6}-\d$", ErrorMessage = "NIP must have format XX-123-456789-5.")]
        public required string NIP { get; set; }
        public required string Address { get; set; }
        [RegularExpression(@"\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone Number must have format: 123-456-789.")]
        public required string PhoneNumber { get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must have format XX-XXX.")]
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public DateTime? Subscription { get; set; }

        [JsonIgnore]
        public virtual List<Order>? Orders { get; set; }
        [JsonIgnore]
        public virtual List<Product>? Products { get; set; }
        [JsonIgnore]
        public virtual List<Account>? Accounts { get; set; }
        [JsonIgnore]
        public virtual List<Warehouse>? Warehouses { get; set; }
    }
}
