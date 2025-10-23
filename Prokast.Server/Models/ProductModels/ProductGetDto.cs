using Prokast.Server.Entities;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductGetDto
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string EAN { get; set; }
        public string Description { get; set; }
        public List<AdditionalDescriptionCreateDto>? AdditionalDescriptions { get; set; }
        public List<AdditionalNameDto>? AdditionalNames { get; set; }
        public List<DictionaryParams>? DictionaryParams { get; set; }
        public List<CustomParamsDto>? CustomParams { get; set; }
        public List<PhotoGetDto>? Photos { get; set; }
        public PriceListsCreateDto? PriceList { get; set; }
    }
}
