using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PricesModels;
using Prokast.Server.Models.ProductModels.ProductGetComponentsModels;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductMappingProfile: Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductGetDto>();
            CreateMap<AdditionalDescription, AdditionalDescriptionProductGetDto>();
            CreateMap<AdditionalName, AdditionalNameProductGetDto>();
            CreateMap<DictionaryParams, DictionaryParams>();
            CreateMap<CustomParams, CustomParamsProductGetDto>();
            CreateMap<Photo, PhotoGetDto>();
            CreateMap<Prices, PriceProductGetDto>();
            CreateMap<PriceList, PriceListProductGetDto>();
        }
    }
}
