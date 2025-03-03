using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string OrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [RegularExpression("^(pending|processing|shipped|delivered|cancelled|returned)$", ErrorMessage = "Status musi mieć jedną z dozwolonych wartości.")]
        public string OrderStatus { get; set; } = "pending";
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal TotalWeightKg { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        [RegularExpression("^(pending|paid|failed|refunded)$", ErrorMessage = "PaymentStatus musi mieć jedną z dozwolonych wartości.")]
        public string PaymentStatus { get; set; } = "pending";
        [Required]
        public DateTime UpdateDate { get; set; }
        public string? TrackingID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public int? BusinessID { get; set; }
        [Required]
        public bool IsBusiness { get; set; }
    }
}
