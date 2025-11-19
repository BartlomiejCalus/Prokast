using Prokast.Server.Entities;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.ProductModels.ProductGetComponentsModels;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductGetDto
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string EAN { get; set; }
        public string Description { get; set; }
        public List<AdditionalDescriptionProductGetDto>? AdditionalDescriptions { get; set; }
        public List<AdditionalNameProductGetDto>? AdditionalNames { get; set; }
        public List<DictionaryParams>? DictionaryParams { get; set; }
        public List<CustomParamsProductGetDto>? CustomParams { get; set; }
        public List<PhotoGetDto>? Photos { get; set; }
        public PriceListProductGetDto? PriceList { get; set; }
    }
}
