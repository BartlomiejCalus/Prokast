using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public Response CreateAdditionalDescription([FromBody] AdditionalDescriptionCreateDto description, int clientID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            
            if (description == null)
            {
                
                return responseNull;
            }

            if(_dbContext.AdditionalDescriptions.Any(x => x.Title == description.Title && x.ClientID == clientID))
            {
                responseNull.errorMsg = "Nazwa jest zajęta!";
                return responseNull;
            }

            var additionalDescription = new AdditionalDescription()
            {
                ClientID = clientID,
                Title = description.Title,
                Region = description.Region,
                Value = description.Value,
            };

            _dbContext.AdditionalDescriptions.Add(additionalDescription);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllDescriptions(int clientID)
        {
            var addDescList = _dbContext.AdditionalDescriptions.Where(x => x.ClientID == clientID).ToList();
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
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.ID == ID && x.ClientID == clientID).ToList();
            var response = new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), Model = addDesc };
            if (addDesc.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;
        }

        public Response GetDescriptionsByNames(string Title, int clientID)
        {
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.Title.Contains(Title) && x.ClientID == clientID).ToList();
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
            var addDesc = _dbContext.AdditionalDescriptions.Where(x => x.Region == Region && x.ClientID == clientID).ToList();
            var response = new AdditionalDescriptionGetResponse() { ID = random.Next(1, 100000), Model = addDesc };
            if (addDesc.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }
        #endregion

        #region Edit
        public Response EditAdditionalDescription(int clientID, int ID, AdditionalDescriptionCreateDto data)
        {
            var findDescription = _dbContext.AdditionalDescriptions.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);

            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
            if (findDescription == null)
            {
                return responseNull;
            }

            if (_dbContext.AdditionalDescriptions.Any(x => x.Title == data.Title && x.ClientID == clientID))
            {
                responseNull.errorMsg = "Nazwa jest zajęta!";
                return responseNull;
            }

            findDescription.Title = data.Title;
            findDescription.Region = data.Region;
            findDescription.Value = data.Value;
            _dbContext.SaveChanges();

            var response = new AdditionalDescriptionEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, AdditionalDescriptionEdit = findDescription };

            return response;
        }
        #endregion

        #region delete
        public Response DeleteAdditionalDescription(int clientID, int ID)
        {
            var findAdditionalDescription = _dbContext.AdditionalDescriptions.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);


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
