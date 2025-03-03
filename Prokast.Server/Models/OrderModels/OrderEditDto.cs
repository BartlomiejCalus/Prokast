using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.OrderModels
{
    public class OrderEditDto
    {
        [Required]
        public string OrderID { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal TotalWeightKg { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public int BusinessID { get; set; }
    }
}
