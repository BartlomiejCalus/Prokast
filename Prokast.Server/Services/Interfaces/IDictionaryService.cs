using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IDictionaryService
    {

        Response GetAllParams();
        Response GetParamsByID(int ID);
        Response GetParamsByName(string name);
        Response GetParamsByRegion(int region);
        Response GetValuesByName(string name);
        Response GetAllParamsInProduct(int clientID, int productID);
    }
}
