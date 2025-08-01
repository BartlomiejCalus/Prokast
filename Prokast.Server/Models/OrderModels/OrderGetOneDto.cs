﻿using Prokast.Server.Enums;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.OrderModels
{
    public class OrderGetOneDto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string OrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [RegularExpression("^(pending|processing|shipped|delivered|cancelled|returned)$", ErrorMessage = "Status musi mieć jedną z dozwolonych wartości.")]
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal TotalWeightKg { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        [RegularExpression("^(pending|paid|failed|refunded)$", ErrorMessage = "PaymentStatus musi mieć jedną z dozwolonych wartości.")]
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public string TrackingID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int BuyerID { get; set; }
        [Required]
        public bool IsBusiness { get; set; }
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
        public string? HouseNumber { get; set; }
        [Required]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must have format XX-XXX.")]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string? BusinessFirstName { get; set; }
        public string? BusinessLastName { get; set; }
        public string? BusinessEmail { get; set; }
        public string? BusinessPhoneNumber { get; set; }
        public string? BusinessAddress { get; set; }
        public string? BusinessHouseNumber { get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must have format XX-XXX.")]
        public string? BusinessPostalCode { get; set; }
        public string? BusinessCity { get; set; }
        public string? BusinessCountry { get; set; }
        [RegularExpression(@"^[A-Z]{2}-\d{3}-\d{6}-\d$", ErrorMessage = "NIP must have format XX-123-456789-5.")]
        public string? NIP { get; set; }
    }
}
