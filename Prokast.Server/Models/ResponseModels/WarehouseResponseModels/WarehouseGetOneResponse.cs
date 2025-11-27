using Prokast.Server.Entities;
using Prokast.Server.Models.WarehouseModels;

namespace Prokast.Server.Models.ResponseModels.WarehouseResponseModels
{
    public class WarehouseGetOneResponse: Response
    {
        public WarehouseGetAllData Model { get; set; }
    }
}
