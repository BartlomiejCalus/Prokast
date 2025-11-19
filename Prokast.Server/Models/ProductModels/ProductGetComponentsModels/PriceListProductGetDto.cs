using Prokast.Server.Entities;

namespace Prokast.Server.Models.ProductModels.ProductGetComponentsModels
{
    public class PriceListProductGetDto
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public virtual List<Prices> Prices { get; set; }

    }
}
