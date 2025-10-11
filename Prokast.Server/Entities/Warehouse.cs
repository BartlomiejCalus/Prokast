using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class Warehouse
    {
        [Key]
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must have format XX-XXX.")]
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        [RegularExpression(@"\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone Number must have format: 123-456-789.")]
        public required string PhoneNumber { get; set; }

        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        [JsonIgnore]
        public virtual List<StoredProduct> StoredProducts { get; set; }

        [JsonIgnore]
        public virtual List<Account> Accounts { get; set; }
    }
}
