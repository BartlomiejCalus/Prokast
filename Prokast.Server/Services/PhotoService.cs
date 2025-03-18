using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.PhotoResponseModels;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public PhotoService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Get
        public Response GetAllPhotos(int clientID)
        {
            var photoList = _dbContext.Photos.Where(x => x.ClientID == clientID).ToList();
            var response = new PhotoGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = photoList };
            if (photoList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma zdjęć" };
                return responseNull;
            }
            return response;
        }

        public Response GetPhotosByID(int clientID, int ID)
        {
            var param = _dbContext.Photos.Where(x => x.ClientID == clientID && x.Id == ID).ToList();
            var response = new PhotoGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego zdjęcia" };
                return responseNull;
            }
            return response;

        }
        #endregion

        #region Edit
        public Response EditPhotos(int clientID, int ID, PhotoEdit data)
        {
            var findPhoto = _dbContext.Photos.FirstOrDefault(x => x.ClientID == clientID && x.Id == ID);

            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego zdjęcia!" };
            if (findPhoto == null)
            {
                return responseNull;
            }

            if (_dbContext.Photos.Any(x => x.Name == data.Name))
            {
                responseNull.errorMsg = "Nazwa jest zajęta!";
                return responseNull;
            }

            findPhoto.Name = data.Name;
            
            _dbContext.SaveChanges();

            var response = new PhotoEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, photo = findPhoto };

            return response;
        }
        #endregion

        #region delete
        public Response DeletePhotos(int clientID, int ID)
        {
            var findPhoto = _dbContext.Photos.FirstOrDefault(x => x.ClientID == clientID && x.Id == ID);


            if (findPhoto == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            _dbContext.Remove(findPhoto);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };

            return response;
        }
        #endregion
    }
}
