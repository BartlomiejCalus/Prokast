using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Models.ClientModels;

namespace Prokast.Server.Services.Interfaces
{
    public interface IClientService
    {
        Response RegisterClient(Registration registration);
    }
}
