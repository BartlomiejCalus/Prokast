using Prokast.Server.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prokast.Server.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public required string OrderID { get; set; }
        public required DateTime OrderDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required OrderStatus OrderStatus { get; set; } = OrderStatus.pending;
        public required decimal TotalPrice { get; set; }
        public required decimal TotalWeightKg { get; set; }
        public required string PaymentMethod { get; set; }
        public required PaymentStatus PaymentStatus { get; set; } = PaymentStatus.pending;
        public required DateTime UpdateDate { get; set; }
        public string? TrackingID { get; set; }
        public required bool IsBusiness { get; set; }
        public int? BusinessID { get; set; }

        public required int ClientID { get; set; }
        public virtual Client Client { get; set; }

        public required int BuyerID { get; set; }
        public virtual Buyer Buyer { get; set; }

        [JsonIgnore]
        public virtual List<OrderProduct>? OrderProducts  { get; set; } = [];
    }
}
