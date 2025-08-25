using Prokast.Server.Entities;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PricesModels;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string EAN { get; set; }
        [Required]
        public string Description { get; set; }
        public List<AdditionalDescriptionCreateDto>? AdditionalDescriptions { get; set; }
        public List<AdditionalNameDto>? AdditionalNames { get; set; }
        public List<DictionaryParams>? DictionaryParams { get; set; }
        public List<CustomParamsDto>? CustomParams { get; set; }

        public List<PhotoDto>? Photos { get; set; }
        /*[Required]
        public List<PricesDto>? Prices { get; set; }*/
        [Required]
        public PriceListsCreateDto? PriceList { get; set; }
    }
}
