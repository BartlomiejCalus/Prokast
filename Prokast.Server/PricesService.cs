using AutoMapper;
using Azure;
using Prokast.Server.Entities;
using Prokast.Server.Models.PriceModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PricesModels;
using Prokast.Server.Models.ResponseModels;
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
        public Response CreatePriceList([FromBody] PriceListsCreateDto priceLists, int clientID)
        {
            if (priceLists == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
                return responseNull;
            }
            var priceList = new PriceLists
            {
                Name = priceLists.Name.ToString(),
                ClientID = clientID
            };
            _dbContext.PriceLists.Add(priceList);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }

        public Response CreatePrice([FromBody] PricesDto prices, int priceListID, int clientID)
        {
            //var input = _mapper.Map<PricesDto>(prices);
            if (prices == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
                return responseNull;
            }

            var list = _dbContext.PriceLists.FirstOrDefault(x => x.ID == priceListID);
            if (list == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiej listy" };
                return responseNull;
            }

            var price = new Prices
            {
                Name = prices.Name.ToString(),
                RegionID = prices.RegionID,
                Netto = prices.Netto,
                VAT = prices.VAT,
                Brutto = prices.Brutto,
                PriceListID = priceListID,
            };

            _dbContext.Prices.Add(price);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllPriceLists(int clientID)
        {
            var priceList = _dbContext.PriceLists.Where(x => x.ClientID == clientID).ToList();
            if (priceList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów" };
                return responseNull;
            }

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
            var response = new PriceListsGetResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = priceListList };
            return response;
        }

        public Response GetPriceListsByName (int clientID, string name)
        {
            var priceList = _dbContext.PriceLists.Where(x =>x.ClientID == clientID && x.Name.Contains(name)).ToList();
            if (priceList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów o takiej nazwie" };
                return responseNull;
            }

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
            var response = new PriceListsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = priceListList };
            return response;
        }

        public Response GetAllPrices(int clientID, int priceListID)
        {
            var priceList = _dbContext.Prices.Where(x => x.PriceListID == priceListID).ToList();
            if (priceList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Cennik nie ma cen lub nie istnieje" };
                return responseNull;
            }
            var response = new PricesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = priceList };
            return response;
        }

        public Response GetPricesByRegion (int clientID,int priceListID, int regionID)
        {
            var priceList = _dbContext.Prices.Where(x => x.PriceListID==priceListID && x.RegionID == regionID).ToList();
            if (priceList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Cennik nie ma cen lub nie istnieje" };
                return responseNull;
            }
            var response = new PricesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = priceList };
            return response;
        }

        public Response GetPricesByName (int clientID,int priceListID, string name)
        {
            var priceList = _dbContext.Prices.Where(x => x.PriceListID == priceListID && x.Name.Contains(name)).ToList();
            if (priceList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Cennik nie ma cen lub nie istnieje" };
                return responseNull;
            }
            var response = new PricesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = priceList };
            return response;
        }

        public Response GetAllPricesInProduct(int clientID, int productID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego cennika" };

            var product = _dbContext.Products.FirstOrDefault(x => x.ClientID == clientID && x.ID == productID);
            if (product == null)
            {
                responseNull.errorMsg = "Nie ma takiego produktu!";
                return responseNull;
            }
            var pricelistID = product.PriceListID;

            var prices = _dbContext.Prices.Where(x => x.PriceListID == pricelistID).ToList();

            if (prices.Count() == 0)
            {
                return responseNull;
            }

            var response = new PricesGetResponse() { ID = random.Next(1, 100000), Model = prices };
            return response;

        }
        #endregion

        #region Edit
        public Response EditPrice(EditPriceDto editPriceDto,int clientID, int priceListID, int priceID)
        {
            var price = _dbContext.Prices.FirstOrDefault(x => x.PriceListID == priceListID && x.ID == priceID);
            if (price == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiegj ceny!" };
                return responseNull;
            }

            price.Name = editPriceDto.Name;
            price.Netto = editPriceDto.Netto;
            price.VAT = editPriceDto.VAT;
            price.Brutto = editPriceDto.Brutto;
            
            _dbContext.SaveChanges();

            var response = new PricesEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = editPriceDto };
            return response;

        }
        #endregion

        #region Delete
        public Response DeletePrice(int clientID, int priceListID, int priceID)
        {
            var price = _dbContext.Prices.FirstOrDefault(x => x.PriceListID == priceListID && x.ID == priceID);
            if (price == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiej ceny!" };
                return responseNull;
            }

            _dbContext.Remove(price);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Cena została usunięta" };
            return response;
        }

        public Response DeletePriceList(int clientID, int priceListID)
        {
            var priceList = _dbContext.PriceLists.FirstOrDefault(x => x.ID == priceListID && x.ClientID == clientID);
            if (priceList == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego cennika!" };
                return responseNull;
            }

            var prices = _dbContext.Prices.Where(x => x.PriceListID == priceListID);

            foreach ( var price in prices)
            {
                _dbContext.Remove(price);
            }
            
            _dbContext.Remove(priceList);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Cennik został usunięty" };
            return response;
        }
        #endregion

    }
}
