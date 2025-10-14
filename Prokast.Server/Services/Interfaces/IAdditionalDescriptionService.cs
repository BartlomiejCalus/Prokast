using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IAdditionalDescriptionService
    {
        Response CreateAdditionalDescription(AdditionalDescriptionCreateDto description, int clientID, int regionID, int productID);
        Response GetAllDescriptions(int clientID);
        Response GetDescriptionsByID(int ID, int clientID);
        Response GetDescriptionsByNames(string Title, int clientID);
        Response GetDescriptionByRegion(int Region, int clientID);
        Response GetAllDescriptionsInProduct(int clientID, int productID);
        Response EditAdditionalDescription(int clientID, int ID, AdditionalDescriptionCreateDto data);
        Response DeleteAdditionalDescription(int clientID, int ID);
    }
}
