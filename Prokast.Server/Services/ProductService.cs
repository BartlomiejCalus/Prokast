using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ProductModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.ProductResponseModels;
using Prokast.Server.Models.ResponseModels.PriceResponseModels.PriceListResponseModels;
using Prokast.Server.Services.Interfaces;
using System.Collections.Immutable;
using System.Linq;

namespace Prokast.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();
        private readonly IAdditionalDescriptionService _additionalDescriptionService;
        private readonly IAdditionalNameService _additionalNameService;
        private readonly IParamsService _paramsService;
        private readonly IPhotoService _photoService;
        private readonly IPricesService _priceService;

        public ProductService(ProkastServerDbContext dbContext, IMapper mapper, 
            IAdditionalDescriptionService additionalDescriptionService, 
            IAdditionalNameService additionalNameService,
            IParamsService paramsService,
            IPhotoService photoService,
            IPricesService pricesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _additionalDescriptionService = additionalDescriptionService;
            _additionalNameService = additionalNameService;
            _paramsService = paramsService;
            _photoService = photoService;
            _priceService = pricesService;
        }

        #region Create
        /*public Response CreateProduct([FromBody] ProductCreateDto productCreateDto, int clientID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (productCreateDto == null)
            {
                return responseNull;
            }
            List<string> AdditionalNamesTitles = new List<string>();
            foreach (var item in productCreateDto.AdditionalNames)
            {
                AdditionalNamesTitles.Add(item.Title);
            }

            var product = new Product
            {
                ClientID = clientID,
                Name = productCreateDto.Name,
                SKU = productCreateDto.SKU,
                EAN = productCreateDto.EAN,
                Description = productCreateDto.Description,
                AdditionalNames = "-1",
                DictionaryParams = "-1",
                CustomParams = "-1",
                Photos = "-1",
                PriceListID = -1

            };

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            var newProduct = _dbContext.Products.FirstOrDefault(x => x.ClientID == clientID && x.Name == productCreateDto.Name);

            foreach (var item in productCreateDto.AdditionalNames)
            {
                var name = new AdditionalName
                {
                    ClientID = clientID,
                    Title = item.Title.ToString(),
                    Region = item.Region,
                    Value = item.Value.ToString()
                };
                _dbContext.AdditionalNames.Add(name);
                _dbContext.SaveChanges();
            }

            foreach (var item in productCreateDto.CustomParams)
            {
                var param = new CustomParams
                {
                    Name = item.Name.ToString(),
                    Type = item.Type.ToString(),
                    Value = item.Value.ToString(),
                    ClientID = clientID
                };
                _dbContext.CustomParams.Add(param);
                _dbContext.SaveChanges();
            }

            foreach (var item in productCreateDto.Photos)
            {
                var param = new Photo
                {
                    Name = item.Name.ToString(),


                    Value = item.Value.ToString(),
                    ProductID = newProduct.ID,
                    ClientID = clientID
                };
                _dbContext.Photos.Add(param);
                _dbContext.SaveChanges();
            }


            var priceList = new PriceLists
            {
                Name = productCreateDto.PriceList.Name,
                ClientID = clientID,
            };
            _dbContext.PriceLists.Add(priceList);
            _dbContext.SaveChanges();

            var newPriceList = _dbContext.PriceLists.FirstOrDefault(x => x.Name == productCreateDto.PriceList.Name && x.ClientID == clientID);
            foreach (var item in productCreateDto.Prices)
            {
                var price = new Prices
                {
                    Name = item.Name,
                    RegionID = item.RegionID,
                    Netto = item.Netto,
                    VAT = item.VAT,
                    Brutto = item.Brutto,
                    PriceListID = newPriceList.ID
                };
                _dbContext.Prices.Add(price);
                _dbContext.SaveChanges();
            }

            List<int> additionalNames = new List<int>();
            foreach (var item in productCreateDto.AdditionalNames)
            {
                additionalNames.Add(_dbContext.AdditionalNames.FirstOrDefault(x => x.Title == item.Title && x.ClientID == clientID).ID);
            }

            List<int> dictionaryParams = new List<int>();
            foreach (var item in productCreateDto.DictionaryParams)
            {
                dictionaryParams.Add(_dbContext.DictionaryParams.FirstOrDefault(x => x.Name == item.Name).ID);


            }

            List<int> photos = new List<int>();
            foreach (var item in productCreateDto.Photos)
            {
                photos.Add(_dbContext.Photos.FirstOrDefault(x => x.Name == item.Name && x.ClientID == clientID).ID);
            }

            List<int> customParams = new List<int>();
            foreach (var item in productCreateDto.CustomParams)
            {
                customParams.Add(_dbContext.CustomParams.FirstOrDefault(x => x.Name == item.Name && x.ClientID == clientID).ID);
            }

            newProduct.AdditionalNames = string.Join(",", additionalNames);
            newProduct.DictionaryParams = string.Join(",", dictionaryParams);
            newProduct.CustomParams = string.Join(",", customParams);
            newProduct.Photos = string.Join(",", photos);
            newProduct.PriceListID = newPriceList.ID;

            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }*/

        public Response CreateProduct(ProductCreateDto productCreateDto, int clientID, int regionID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (productCreateDto == null)
            {
                return responseNull;
            }

            var client = _dbContext.Clients.FirstOrDefault(x => x.ID == clientID);
            if (client == null)
            {
                responseNull.errorMsg = "Nie ma takiego klienta!";
                return responseNull;
            }

            var product = new Product
            {
                ClientID = clientID,
                Name = productCreateDto.Name,
                SKU = productCreateDto.SKU,
                EAN = productCreateDto.EAN,
                Description = productCreateDto.Description,
            };

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            var newProduct = _dbContext.Products.FirstOrDefault(x => x.Name == productCreateDto.Name && x.SKU == productCreateDto.SKU && x.EAN == productCreateDto.EAN);   

            foreach(var additionalDescription in productCreateDto.AdditionalDescriptions)
            {
                var result = _additionalDescriptionService.CreateAdditionalDescription(additionalDescription, clientID, regionID, newProduct.ID);
                if (result != null && result.GetType() == typeof(ErrorResponse))
                {
                    return result;
                }
            }

            foreach(var additionalName in productCreateDto.AdditionalNames)
            {
               var result = _additionalNameService.CreateAdditionalName(additionalName, clientID, regionID, newProduct.ID);
                if (result != null && result.GetType() == typeof(ErrorResponse))
                {
                    return result;
                }

            }

            foreach(var customParam in productCreateDto.CustomParams)
            {
                var result = _paramsService.CreateCustomParam(customParam, clientID, regionID, newProduct.ID);
                if (result != null && result.GetType() == typeof(ErrorResponse))
                {
                    return result;
                }
            }

            foreach(var dictionaryParam in productCreateDto.DictionaryParams)
            {
                var param = _dbContext.DictionaryParams.FirstOrDefault(x => x.ID == dictionaryParam.ID);
                newProduct.DictionaryParams.Add(param);
                _dbContext.SaveChanges();
            }

            foreach (var photo in productCreateDto.Photos)
            {
                var result = _photoService.CreatePhoto(photo, clientID, newProduct.ID);
                if (result != null && result.GetType() == typeof(ErrorResponse))
                {
                    return result;
                }
            }

            var priceResult = _priceService.CreatePriceList(productCreateDto.PriceList, clientID, newProduct.ID);
            if (priceResult != null && priceResult.GetType() == typeof(ErrorResponse))
            {
                return priceResult;
            }
            foreach (var price in productCreateDto.PriceList.Prices)
            {
                var result = _priceService.CreatePrice(price,  newProduct.ID, clientID);
                if (result != null && result.GetType() == typeof(ErrorResponse))
                {
                    return result;
                }
            }

            var createdProduct = _dbContext.Products.OrderByDescending(x => x.ID).FirstOrDefault();
            if (createdProduct == null)
            {
                responseNull.errorMsg = "Błąd produktu!";
                return responseNull;
            }

            client.Products.Add(createdProduct);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }




        #endregion

        #region Get
        public Response GetProducts(/*ProductGetFilter filter*/ int clientID, string name, string sku)
        {

            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak produktów!" };
            var products = _dbContext.Products.Where(x => x.ClientID == clientID &&
                                                    (string.IsNullOrEmpty(name) || x.Name.Contains(name)) &&
                                                    (string.IsNullOrEmpty(sku) || x.SKU.Contains(sku))
                ).Select(x => new {x.ID, x.Name, x.SKU, x.Photos, x.AdditionDate }).ToList();

            if (products.Count() == 0)
            {
                return responseNull;
            }

            var productList = new List<ProductGetMin>();
            foreach (var product in products)
            {
                var newProductToList = new ProductGetMin
                {
                    ID = product.ID,
                    Name = product.Name,
                    SKU = product.SKU,
                    AdditionDate = product.AdditionDate,
                    Photo = product.Photos.FirstOrDefault().Value
                };
                productList.Add(newProductToList);
            }
            var response = new ProductGetMinResponse { ID = random.Next(1, 100000), ClientID = clientID, Model = productList };
            return response;

            /*var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            var products = _dbContext.Products.Where(x => x.ClientID == clientID).ToList();
            if (!products.Any())
            {
                responseNull.errorMsg = "Klient nie ma produktów";
                return responseNull;
            }
            
            if (filter == null)
            {
                return responseNull;
            }

            if (filter.ProductIDList != null && filter.ProductIDList.Count != 0)
            {
                products = products.Where(x => filter.ProductIDList.Contains(x.ID)).ToList();
            }

            if (filter.CreationDate != null)
            {
                products = products.Where(x => x.AdditionDate < filter.CreationDate).ToList();
            }

            if (filter.ModificationDate != null)
            {
                products = products.Where(x => x.ModificationDate < filter.ModificationDate).ToList();
            }

            if (filter.ProductName != null)
            {
                products = products.Where(x => x.Name.Contains(filter.ProductName)).ToList();
            }

            var response = new ProductsGetResponse() { ID = random.Next(0, 100000), ClientID = clientID, Model = products };
            return response;*/

        }
        /*public Response GetProducts([FromBody] ProductGetFilter productGetFilter, int clientID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            var products = _dbContext.Products.Where(x => x.ClientID == clientID);

            if (productGetFilter == null)
            {
                return responseNull;
            }

            if (!products.Any())
            {
                responseNull.errorMsg = "Klient nie ma produktów";
                return responseNull;
            }

            if (productGetFilter.ProductIDList != null && productGetFilter.ProductIDList.Count != 0)
            {
                products = products.Where(x => productGetFilter.ProductIDList.Contains(x.ID));
            }

            if (productGetFilter.CreationDate != null)
            {
                products = products.Where(x => x.AdditionDate < productGetFilter.CreationDate);
            }

            if (productGetFilter.ModificationDate != null)
            {
                products = products.Where(x => x.ModificationDate < productGetFilter.ModificationDate);
            }

            if (productGetFilter.ProductName != null)
            {
                products = products.Where(x => x.Name.Contains(productGetFilter.ProductName));
            }

            var productList = products.ToList();

            var returnList = new List<ProductGet>();

            var additionalNames = _dbContext.AdditionalNames.ToList();
            var customParams = _dbContext.CustomParams.ToList();
            var dictionaryParams = _dbContext.DictionaryParams.ToList();
            var priceList = _dbContext.PriceLists.ToList();
            var prices = _dbContext.Prices.ToList();
            foreach (var product in productList)
            {
                var productAdditionalNames = additionalNames.Where(x => product.AdditionalNames.Split(",").ToList().
                    Contains(x.ID.ToString())).ToList();

                var productCustomParams = customParams.Where(x => product.CustomParams.Split(",").ToList().
                    Contains(x.ID.ToString())).ToList();

                var productDictionaryParams = dictionaryParams.Where(x => product.DictionaryParams.Split(",").ToList().
                    Contains(x.ID.ToString())).ToList();

                var productPriceLists = priceList.FirstOrDefault(x => x.ID == product.PriceListID);

                var productPrices = prices.Where(x => x.PriceListID == productPriceLists.ID).ToList();

                returnList.Add(new ProductGet()
                {
                    ID = product.ID,
                    ClientID = clientID,
                    Name = product.Name,
                    SKU = product.SKU,
                    EAN = product.EAN,
                    Description = product.Description,
                    AdditionalNames = productAdditionalNames,
                    DictionaryParams = productDictionaryParams,
                    CustomParams = productCustomParams,
                    PriceList = new PriceListAll()
                    {
                        ID = productPriceLists.ID,
                        Name = productPriceLists.Name,
                        ClientID = clientID,
                        Prices = productPrices
                    },
                    AdditionDate = product.AdditionDate,
                    ModificationDate = product.ModificationDate
                });

            }
            var response = new ProductsGetResponse() { ID = random.Next(0, 100000), ClientID = clientID, Model = returnList };
            return response;

        }*/
        #endregion

        #region Delete

        public DeleteResponse DeleteProduct (int clientID, int productID)
        {
            var responseNull = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Nie ma takiego produktu!" };

            var product = _dbContext.Products.FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
            {
                return responseNull;
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Produkt został usunięty" };
            return response;
        }

        /*public DeleteResponse DeleteProduct(int clientID, int productID)
        {
            var products = _dbContext.Products.FirstOrDefault(x => x.ClientID == clientID && x.ID == productID);
            if (products == null)
            {
                var responseNull = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Nie ma takiego produktu!" };
                return responseNull;
            }

            var prices = _dbContext.Prices.ToList();

            var _priceList = _dbContext.PriceLists.ToList();

            var additionalNames = _dbContext.AdditionalName.ToList();

            var productAdditionalNames = additionalNames.Where(x => products.AdditionalNames.Split(",").ToList().
                Contains(x.ID.ToString())).ToList();

            var productPriceLists = _priceList.FirstOrDefault(x => x.ID == products.PriceListID);

            var productPrices = prices.Where(x => x.PriceListID == productPriceLists.ID).ToList();

            foreach (var item in productAdditionalNames)
            {
                _dbContext.Remove(item);
            }
            foreach (var item in productPrices)
            {
                _dbContext.Remove(item);

            }
            _dbContext.Remove(productPriceLists);
            _dbContext.Remove(products);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Produkt został usunięty" };
            return response;

        }*/

        #endregion

        #region Edit
        public Response EditProduct(ProductEdit productEdit, int clientID, int productID)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                return responseNull;
            }

            product.Name = productEdit.Name;
            product.SKU = productEdit.SKU;
            product.EAN = productEdit.EAN;
            product.Description = productEdit.Description;
            product.ModificationDate = DateTime.Now;
            _dbContext.SaveChanges();

            var response = new ProductEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = productEdit };
            return response;

        }

        #endregion

    }
}
