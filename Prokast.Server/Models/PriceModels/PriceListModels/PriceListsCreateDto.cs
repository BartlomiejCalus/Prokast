using Prokast.Server.Models.PricesModels;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.PriceModels.PriceListModels
{
    public class PriceListsCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public List<PricesDto>? Prices { get; set; }

    }
}
