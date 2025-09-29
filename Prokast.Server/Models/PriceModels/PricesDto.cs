using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.PricesModels
{
    public class PricesDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int RegionID { get; set; }
        [Required]
        public decimal Netto { get; set; }
        [Required]
        public decimal VAT { get; set; }
        [Required]
        public decimal Brutto { get; set; }
    }
}
