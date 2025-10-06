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
        public Response CreateAdditionalDescription([FromBody] AdditionalDescriptionCreateDto description, int clientID, int regionID, int productID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (description == null)
            {
                return responseNull;
            }
            var product = _dbContext.Products.Include(p => p.AdditionalDescriptions).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
            {
                responseNull.errorMsg = "Nie ma takiego produktu!";
                return responseNull;
            }

            var additionalDescription = new AdditionalDescription()
            {
                Title = description.Title,
                Value = description.Value,
                RegionID = regionID,
                ProductID = productID
            };
            
            product.AdditionalDescriptions.Add(additionalDescription);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllDescriptions(int clientID)
        {
            var addDescList = _dbContext.AdditionalDescriptions.Where(x => x.Product.ClientID == clientID).ToList();
            var response = new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), Model = addDescList };
            if (addDescList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };
                return responseNull;
            }
            return response;
        }

        public Response GetDescriptionsByID(int ID, int clientID)
        {
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.ID == ID && x.Product.ClientID == clientID).ToList();
            var response = new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), Model = addDesc };
            if (addDesc.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;
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
            var response = new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), Model = addDesc };
            if (addDesc.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }

        public Response GetDescriptionByRegion(int Region, int clientID)
        {
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.RegionID == Region && x.Product.ClientID == clientID).ToList();
            var response = new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), Model = addDesc };
            if (addDesc.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }

       /* public Response GetAllDescriptionsInProduct(int clientID, int productID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };

            var product = _dbContext.Products.FirstOrDefault(x => x.ClientID == clientID && x.ID == productID);
        }
*/
        #endregion

        #region Edit
        public Response EditAdditionalDescription(int clientID, int ID, AdditionalDescriptionCreateDto data)
        {
            var findDescription = _dbContext.AdditionalDescriptions.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findDescription == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            findDescription.Title = data.Title;
            //findDescription.Region = data.Region;
            findDescription.Value = data.Value;
            _dbContext.SaveChanges();

            var response = new AdditionalDescriptionEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, AdditionalDescriptionEdit = findDescription };

            return response;
        }
        #endregion

        #region delete
        public Response DeleteAdditionalDescription(int clientID, int ID)
        {
            var findAdditionalDescription = _dbContext.AdditionalDescriptions.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findAdditionalDescription == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            _dbContext.AdditionalDescriptions.Remove(findAdditionalDescription);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };

            return response;
        }
        #endregion
    }
}
