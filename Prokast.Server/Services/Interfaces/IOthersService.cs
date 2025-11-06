using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IOthersService
    {
        Response GetRegions();
        Response GetMainPage(int clientID);
    }
}
