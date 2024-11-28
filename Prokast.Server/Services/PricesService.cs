﻿using AutoMapper;
using Azure;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
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
            var input = _mapper.Map<PriceListsCreateDto>(priceLists);
            if (input == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
                return responseNull;
            }
            var priceList = new PriceLists
            {
                Name = input.Name.ToString(),
                ClientID = clientID
            };
            _dbContext.PriceLists.Add(priceList);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }

        public Response CreatePrice([FromBody] PricesDto prices, int priceListID, int clientID)
        {
           
            
            var input = _mapper.Map<PricesDto>(prices);
            if (input == null)
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
                Name = input.Name.ToString(),
                RegionID = input.RegionID,
                Netto = input.Netto,
                VAT = input.VAT,
                Brutto = input.Brutto,
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

            List<PriceListDto> priceListList = new List<PriceListDto>();
            foreach (var price in priceList)
            {
                var prices = _dbContext.Prices.Where(x => x.PriceListID == price.ID).ToList();
                PriceListDto priceListDto = new PriceListDto()
                {
                    Name = price.Name,
                    Prices = prices
                };
                priceListList.Add(priceListDto);

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

            List<PriceListDto> priceListList = new List<PriceListDto>();
            foreach (var price in priceList)
            {
                var prices = _dbContext.Prices.Where(x => x.PriceListID == price.ID).ToList();
                PriceListDto priceListDto = new PriceListDto()
                {
                    Name = price.Name,
                    Prices = prices
                };
                priceListList.Add(priceListDto);

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
            
            _dbContext.SaveChanges();

            var response = new PricesEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = editPriceDto };
            return response;

        }
        #endregion

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
    }
}
