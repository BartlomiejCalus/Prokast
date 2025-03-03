using Prokast.Server.Models.OrderModels;

namespace Prokast.Server.Models.ResponseModels.OrderResponseModels
{
    public class OrderGetAllResponse: Response
    {
        public List<OrderGetAllDto> Model { get; set; }
    }
}
