using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.PriceModels
{
    public class EditPriceDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Netto { get; set; }
        [Required]
        public decimal Brutto { get; set; }
        [Required]
        public decimal VAT { get; set; }
        [Required]
        public int RegionId { get; set; }
    }
}
