using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;

namespace Prokast.Server.Services
{
    public interface IDictionaryService
    {
        
        Response GetAllParams();
        Response GetParamsByID( int ID);
        Response GetParamsByName( string name);
        
    }
}
