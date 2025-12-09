using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalDescriptionResponseModels;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Services
{
    public class AdditionalDescriptionService : IAdditionalDescriptionService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public AdditionalDescriptionService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Create
        public Response CreateAdditionalDescription(AdditionalDescriptionCreateDto description, int clientID, int productID)
        {
            if (description == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            
            var product = _dbContext.Products.Include(p => p.AdditionalDescriptions).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
            
            var additionalDescription = new AdditionalDescription()
            {
                Title = description.Title,
                Value = description.Value,
                RegionID = description.RegionID,
                Product = product
            };
            
            product.AdditionalDescriptions.Add(additionalDescription);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }
        #endregion

        #region Get
        public Response GetAllDescriptions(int clientID)
        {
            var addDescList = _dbContext.AdditionalDescriptions.Where(x => x.Product.ClientID == clientID).ToList();
            if (addDescList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak parametrów." };

            return new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), Model = addDescList };
        }

        public Response GetDescriptionsByID(int ID, int clientID)
        {
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.ID == ID && x.Product.ClientID == clientID).ToList();
            if (addDesc.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego parametru." };

            return new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = addDesc };
        }

        /// <summary>
        /// Funkcja pokazuje dodatkowe opisy zawierające podane słowo
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Response GetDescriptionsByNames(string Title, int clientID)
        {
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.Title.Contains(Title) && x.Product.ClientID == clientID).ToList();
            if (addDesc.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego parametru." };

            return new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = addDesc };

        }

        public Response GetDescriptionByRegion(int Region, int clientID)
        {
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.RegionID == Region && x.Product.ClientID == clientID).ToList();
            if (addDesc.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego parametru." };

            return new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = addDesc };

        }

        public Response GetAllDescriptionsInProduct(int clientID, int productID)
        {

            var descriptions = _dbContext.AdditionalDescriptions.Where(x => x.ProductID == productID).ToList();
            if (descriptions is null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błąd przy pobieraniu parametrów." };

            return new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = descriptions };
        }

        #endregion

        #region Edit
        public Response EditAdditionalDescription(int clientID, int ID, AdditionalDescriptionCreateDto data)
        {
            var findDescription = _dbContext.AdditionalDescriptions.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findDescription == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
            

            findDescription.Title = data.Title;
            findDescription.Value = data.Value;
            _dbContext.SaveChanges();

            return new AdditionalDescriptionEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, AdditionalDescriptionEdit = findDescription };
        }
        #endregion

        #region delete
        public Response DeleteAdditionalDescription(int clientID, int ID)
        {
            var findAdditionalDescription = _dbContext.AdditionalDescriptions.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findAdditionalDescription == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
            
            _dbContext.AdditionalDescriptions.Remove(findAdditionalDescription);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };
        }
        #endregion
    }
}
