using Prokast.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.PriceModels.PriceListModels
{
    public class PriceListGet
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public List<Prices> Prices { get; set; }
    }
}
