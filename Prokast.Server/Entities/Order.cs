using Prokast.Server.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public required string OrderID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.pending;
        public required decimal TotalPrice { get; set; }
        public required decimal TotalWeightKg { get; set; }
        public required string PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.pending;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public string? TrackingID { get; set; }
        public required bool IsBusiness { get; set; }
        public int? BusinessID { get; set; }

        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        public int BuyerID { get; set; }
        public virtual Buyer Buyer { get; set; }

        [JsonIgnore]
        public virtual List<OrderProduct>? OrderProducts  { get; set; } = [];
    }
}
