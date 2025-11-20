using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models.PriceModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PricesModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Models.ResponseModels.PhotoResponseModels;
using Prokast.Server.Models.ResponseModels.PriceResponseModels;
using Prokast.Server.Models.ResponseModels.PriceResponseModels.PriceListResponseModels;
using Prokast.Server.Services.Interfaces;
using System.Web.Http;
using Response = Prokast.Server.Models.Response;

namespace Prokast.Server.Services
{
    public class PricesService: IPricesService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public PricesService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Create
        public Response CreatePriceList([FromBody] PriceListsCreateDto priceLists, int clientID, int productID)
        {
            if (priceLists == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var product = _dbContext.Products.Include(p => p.PriceList).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            var priceList = new PriceList
            {
                Name = priceLists.Name.ToString(),
                Product = product
            };
            product.PriceList = priceList;
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }

        public Response CreatePrice([FromBody] PricesDto prices, int productID, int clientID)
        {
            if (prices == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var priceList = _dbContext.PriceLists.Include(p => p.Prices).FirstOrDefault(x => x.ProductID == productID && x.Product.ClientID == clientID);
            if (priceList == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiej listy!" };

            var price = new Prices
            {
                Name = prices.Name,
                Netto = prices.Netto,
                VAT = prices.VAT,
                Brutto = prices.Brutto,
                RegionID = prices.RegionID,
                PriceLists = priceList
            };

            priceList.Prices.Add(price);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }
        #endregion

        #region Get
        public Response GetAllPriceLists(int clientID)
        {
            var priceList = _dbContext.PriceLists.Where(x => x.Product.ClientID == clientID).ToList();
            if (priceList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów" };

            List<PriceListGet> priceListList = new List<PriceListGet>();
            foreach (var price in priceList)
            {
                var prices = _dbContext.Prices.Where(x => x.PriceListID == price.ID).ToList();
                PriceListGet priceListGet = new PriceListGet()
                {
                    ID = price.ID,
                    Name = price.Name,
                    Prices = prices
                };
                priceListList.Add(priceListGet);

            }
            return new PriceListsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = priceListList };
        }

        public Response GetPriceListsByName (int clientID, string name)
        {
            var priceList = _dbContext.PriceLists.Where(x =>x.Product.ClientID == clientID && x.Name.Contains(name)).ToList();
            if (priceList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów o takiej nazwie" };

            List<PriceListGet> priceListList = new List<PriceListGet>();
            foreach (var price in priceList)
            {
                var prices = _dbContext.Prices.Where(x => x.PriceListID == price.ID).ToList();
                PriceListGet priceListGet = new PriceListGet()
                {
                    ID= price.ID,
                    Name = price.Name,
                    Prices = prices
                };
                priceListList.Add(priceListGet);

            }
            return new PriceListsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = priceListList };
        }

        public Response GetAllPrices(int clientID, int priceListID)
        {
            var priceList = _dbContext.Prices.Where(x => x.PriceLists.ID == priceListID && x.PriceLists.Product.ClientID == clientID).ToList();
            if (priceList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Cennik nie ma cen lub nie istnieje" };
            
            return new PricesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = [.. priceList.Select(x => new PriceGetDto(x))] };
        }

        public Response GetPricesByRegion (int clientID,int priceListID, int regionID)
        {
            var priceList = _dbContext.Prices.Where(x => x.PriceLists.ID==priceListID && x.RegionID == regionID).ToList();
            if (priceList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Cennik nie ma cen lub nie istnieje" };
            
            return new PricesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = [.. priceList.Select(x => new PriceGetDto(x))] };
        }

        public Response GetPricesByName (int clientID,int priceListID, string name)
        {
            var priceList = _dbContext.Prices.Where(x => x.PriceLists.ID == priceListID && x.Name.Contains(name)).ToList();
            if (priceList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Cennik nie ma cen lub nie istnieje" };

            return new PricesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = [.. priceList.Select(x => new PriceGetDto(x))] };
        }

        public Response GetAllPricesInProduct(int clientID, int productId)
        {
            var prices = _dbContext.PriceLists.Where(x => x.ProductID == productId).SelectMany(x => x.Prices).ToList();
            if (prices.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Produkt nie ma tych parametrów!" };

            return new PricesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = [.. prices.Select(x => new PriceGetDto(x))] };
        }
        #endregion

        #region Edit
        public Response EditPrice(EditPriceDto editPriceDto,int clientID, int priceID)
        {
            var price = _dbContext.Prices.FirstOrDefault(x => x.ID == priceID);
            if (price == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiegj ceny!" };

            price.Name = editPriceDto.Name;
            price.Netto = editPriceDto.Netto;
            price.VAT = editPriceDto.VAT;
            price.Brutto = editPriceDto.Brutto;
            price.RegionID = editPriceDto.RegionId;

            _dbContext.SaveChanges();

            return new PricesEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = editPriceDto };

        }
        #endregion

        #region Delete
        public Response DeletePrice(int clientID, int priceID)
        {
            var price = _dbContext.Prices.FirstOrDefault(x => x.ID == priceID);
            if (price == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiej ceny!" };

            _dbContext.Remove(price);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Cena została usunięta" };
        }

        public Response DeletePriceList(int clientID, int priceListID)
        {
            var priceList = _dbContext.PriceLists.FirstOrDefault(x => x.ID == priceListID && x.Product.ClientID == clientID);
            if (priceList == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego cennika!" };

            //var prices = _dbContext.Prices.Where(x => x.PriceListID == priceListID);

            /*foreach ( var price in prices)
            {
                _dbContext.Remove(price);
            }*/
            _dbContext.Remove(priceList);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Cennik został usunięty" };
        }
        #endregion

    }
}
