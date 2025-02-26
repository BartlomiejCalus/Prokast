﻿using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string ShopOrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [RegularExpression("^(pending|processing|shipped|delivered|cancelled|returned)$", ErrorMessage = "Status musi mieć jedną z dozwolonych wartości.")]
        public string OrderStatus { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal TotalWeight { get; set; }
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
    }
}
