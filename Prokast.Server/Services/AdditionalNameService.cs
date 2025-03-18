using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Security.Cryptography;
using System.Linq;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels;
using Prokast.Server.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Prokast.Server.Services
{
    public class AdditionalNameService : IAdditionalNameService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public AdditionalNameService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Create
        public Response CreateAdditionalName([FromBody] AdditionalNameDto additionalNameDto, int clientID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (additionalNameDto == null)
            {
                
                return responseNull;
            }

            if (_dbContext.AdditionalName.Any(x => x.Title == additionalNameDto.Title && x.ClientID == clientID))
            {
                responseNull.errorMsg = "Nazwa jest zajęta!";
                return responseNull;
            }

            var newName = new AdditionalName
            {
                ClientID = clientID,
                Title = additionalNameDto.Title.ToString(),
                Region = additionalNameDto.Region,
                Value = additionalNameDto.Value.ToString()

            };

            _dbContext.AdditionalName.Add(newName);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllNames(int clientID)
        {
            var addNameList = _dbContext.AdditionalName.Where( x => x.ClientID == clientID).ToList();
            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addNameList };
            if (addNameList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };
                return responseNull;
            }
            return response;
        }

        public Response GetNamesByID(int ID, int clientID)
        {
            var addName = _dbContext.AdditionalName.Where(x => x.ID == ID && x.ClientID == clientID).ToList();
            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addName };
            if (addName.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }

        public Response GetNamesByIDNames(int ID, string Title, int clientID)
        {
            var addName = _dbContext.AdditionalName.Where(x => x.ID == ID && x.Title.Contains(Title) && x.ClientID == clientID).ToList();
            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addName };
            if (addName.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }

        public Response GetNamesByIDRegion(int ID, int Region, int clientID)
        {
            var addName = _dbContext.AdditionalName.Where(x => x.ID == ID && x.Region == Region && x.ClientID == clientID).ToList();
            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addName };
            if (addName.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }
        #endregion

        #region Edit
        public Response EditAdditionalName(int clientID, int ID, AdditionalNameDto data)
        {
            var findName = _dbContext.AdditionalName.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);

            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
            if (findName == null)
            {
                return responseNull;
            }

            if (_dbContext.AdditionalName.Any(x => x.Title == data.Title && x.ClientID == clientID))
            {
                responseNull.errorMsg = "Nazwa jest zajęta!";
                return responseNull;
            }

            findName.Title = data.Title;
            findName.Region = data.Region;
            findName.Value = data.Value;
            _dbContext.SaveChanges();

            var response = new AdditionalNameEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, additionalNameEdit = findName };

            return response;
        }
        #endregion

        #region Delete
        public Response DeleteAdditionalName(int clientID, int ID)
        {
            var findAdditionalName = _dbContext.AdditionalName.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);


            if (findAdditionalName == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            _dbContext.AdditionalName.Remove(findAdditionalName);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };

            return response;
        }
        #endregion
    }
}
