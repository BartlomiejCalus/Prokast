using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Models.ResponseModels.PhotoResponseModels;
using Prokast.Server.Services.Interfaces;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Prokast.Server.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobPhotoStorageService _blobPhotoStorageService;
        Random random = new Random();

        public PhotoService(ProkastServerDbContext dbContext, IMapper mapper, IBlobPhotoStorageService blobPhotoStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _blobPhotoStorageService = blobPhotoStorageService;
        }


        public Response CreatePhoto([FromBody] PhotoDto photo, int clientID, int productID) {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            if (photo == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var product = _dbContext.Products.Include(p => p.Photos).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            if (!photo.ContentType.Contains("png") && !photo.ContentType.Contains("jpg"))
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nieobsługiwany typ pliku!" };

            if (photo.ContentType.Contains("png")) { photo.Name = photo.Name + $"_CID{clientID}_PID{photo.ProductId}.png"; }
            else if (photo.ContentType.Contains("jpg")) { photo.Name = photo.Name + $"_CID{clientID}_PID{photo.ProductId}.jpg"; }

            var photoList = _dbContext.Photos.Where(x => x.Product.ClientID == clientID && x.Name== photo.Name).ToList();            
            if (photoList.Count != 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Zdjęcie o takiej nazwie już istnieje!" };
            
            var photoBLOB = new BLOBPhotoModel
            {
                Name = photo.Name,
                Value = photo.Value,
                ContentType = photo.ContentType,
            };
            var link = _blobPhotoStorageService.UploadPhotoAsync(photoBLOB);
            _blobPhotoStorageService.DownloadPhotoAsync(photo.Name);

            var newPhoto = new Photo { 
                Name = photo.Name,
                Product = product,
                Value = link.ToString(),
            };

            product.Photos.Add(newPhoto);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }

        #region Get
        public Response GetAllPhotos(int clientID)
        {
            var photoList = _dbContext.Photos.Where(x => x.Product.ClientID == clientID).ToList();
            if (photoList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma zdjęć" };

            return new PhotoGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = photoList };
        }

        public Response GetPhotosByID(int clientID, int ID)
        {
            var param = _dbContext.Photos.Where(x => x.Product.ClientID == clientID && x.Id == ID).ToList();

            if (param == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego zdjęcia" };
            
            return new PhotoGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };

        }


        public Response GetAllPhotosInProduct(int clientID, int productID)
        {
            var phohoList = _dbContext.Products.FirstOrDefault(x => x.ClientID == clientID && x.ID == productID).Photos;

            if (phohoList.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego zdjecia" };

            return new PhotoGetResponse() { ID = random.Next(1, 100000), Model = phohoList };

        }
        #endregion

        #region Edit
        public Response EditPhotos(int clientID, int ID, PhotoEdit data)
        {
            var findPhoto = _dbContext.Photos.FirstOrDefault(x => x.Product.ClientID == clientID && x.Id == ID);

            if (findPhoto == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego zdjęcia!" };

            findPhoto.Name = data.Name; 
            _dbContext.SaveChanges();

            return new PhotoEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, photo = findPhoto };
        }
        #endregion

        #region delete
        public Response DeletePhotos(int clientID, int ID)
        {
            var findPhoto = _dbContext.Photos.FirstOrDefault(x => x.Product.ClientID == clientID && x.Id == ID);

            if (findPhoto == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };

            _dbContext.Remove(findPhoto);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };
        }
        #endregion
    }
}
