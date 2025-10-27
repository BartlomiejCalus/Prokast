using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PricesModels;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductMappingProfile: Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductGetDto>();
            CreateMap<AdditionalDescription, AdditionalDescriptionCreateDto>();
            CreateMap<AdditionalName, AdditionalNameDto>();
            CreateMap<DictionaryParams, DictionaryParams>();
            CreateMap<CustomParams, CustomParamsDto>();
            CreateMap<Photo, PhotoGetDto>();
            CreateMap<Prices, PricesDto>();
            CreateMap<PriceList, PriceListsCreateDto>();
        }
    }
}
