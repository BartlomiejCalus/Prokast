using Prokast.Server.Models.OrderModels;

namespace Prokast.Server.Models.ResponseModels.OrderResponseModels
{
    public class OrderGetOneResponse: Response
    {
        public OrderGetOneDto Model { get; set; }
    }
}
