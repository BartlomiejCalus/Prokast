using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.OrderModels
{
    public class OrderGetAllDto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string OrderID { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        [Required]
        public string PaymentStatus { get; set; }
    }
}
