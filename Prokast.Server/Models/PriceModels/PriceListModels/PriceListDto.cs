using Prokast.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.PriceModels.PriceListModels
{
    public class PriceListDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public List<Prices> Prices { get; set; }
    }
}
