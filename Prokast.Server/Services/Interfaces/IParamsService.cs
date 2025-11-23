using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IParamsService
    {
        Response CreateCustomParam([FromBody] CustomParamsDto customParamsDto, int clientID, int productID);
        Response GetAllParams(int clientID);
        Response GetParamsByID(int clientID, int ID);
        Response GetParamsByName(int clientID, string name);
        Response GetAllParamsInProduct(int clientID, int productID);
        Response EditParams(int clientID, int ID, CustomParamsDto data);
        Response DeleteParams(int clientID, int ID);
    }
}
