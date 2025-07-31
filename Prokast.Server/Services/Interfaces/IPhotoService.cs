using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.PhotoModels;

namespace Prokast.Server.Services.Interfaces
{
    public interface IPhotoService
    {
        Response GetAllPhotos(int clientID);
        Response GetPhotosByID(int clientID, int ID);
        Response GetAllPhotosInProduct(int clientID, int productID);
        Response EditPhotos(int clientID, int ID, PhotoEdit data);
        Response DeletePhotos(int clientID, int ID);
    }
}
