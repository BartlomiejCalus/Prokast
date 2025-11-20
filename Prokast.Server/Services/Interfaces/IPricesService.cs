using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.PriceModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PricesModels;

namespace Prokast.Server.Services.Interfaces
{
    public interface IPricesService
    {
        Response CreatePriceList(PriceListsCreateDto priceLists, int clientID, int productID);
        Response CreatePrice(PricesDto prices, int productID, int clientID);
        Response GetAllPriceLists(int clientID);
        Response GetPriceListsByName(int clientID, string name);
        Response GetAllPrices(int clientID, int priceListID);
        Response GetPricesByRegion(int clientID, int priceListID, int regionID);
        Response GetPricesByName(int clientID, int priceListID, string name);
        Response GetAllPricesInProduct(int clientID, int pricelistID);
        Response EditPrice(EditPriceDto editPriceDto, int clientID, int priceID);
        Response DeletePrice(int clientID, int priceID);
        Response DeletePriceList(int clientID, int priceListID);
    }
}
