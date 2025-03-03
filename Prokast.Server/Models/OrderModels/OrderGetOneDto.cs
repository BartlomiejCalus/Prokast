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
        public string OrderStatus { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal TotalWeightKg { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        [RegularExpression("^(pending|paid|failed|refunded)$", ErrorMessage = "PaymentStatus musi mieć jedną z dozwolonych wartości.")]
        public string PaymentStatus { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public string TrackingID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"\d{3}-\d{3}-\d{3}$", ErrorMessage = "Phone Number must have format: 123-456-789.")]
        public string PhoneNumber { get; set; }
        public string? BusinessFirstName { get; set; }
        public string? BusinessLastName { get; set; }
        public string? BusinessEmail { get; set; }
        public string? BusinessPhoneNumber { get; set; }
    }
}
