﻿using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Buyer
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
        [Required]
        public string Address { get; set; }
        public string? HouseNumber {get; set; }
        [Required]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must have format XX-XXX.")]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [RegularExpression(@"^[A-Z]{2}-\d{3}-\d{6}-\d$", ErrorMessage = "NIP must have format XX-123-456789-5.")]
        public string? NIP { get; set; }
    }
}
