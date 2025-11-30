using Prokast.Server.Models.WarehouseModels;

namespace Prokast.Server.Models.ResponseModels.WarehouseResponseModels
{
    public class WarehouseGetMinimalResponse: Response
    {
        public List<WarehouseGetMinimal> Model { get; set; }
        public int TotalItems { get; set; }
    }
}
