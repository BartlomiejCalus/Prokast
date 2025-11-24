using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalDescriptionResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels;
using Prokast.Server.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        public Response CreateAdditionalName([FromBody] AdditionalNameDto additionalNameDto, int clientID, int productID)
        {    
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            if (additionalNameDto == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var product = _dbContext.Products.Include(p => p.AdditionalNames).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            var additionalName = new AdditionalName
            {
                Title = additionalNameDto.Title.ToString(),
                Value = additionalNameDto.Value.ToString(),
                RegionID = additionalNameDto.RegionID,
                Product = product
            };

            product.AdditionalNames.Add(additionalName);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }
        #endregion

        #region Get
        public Response GetAllNames(int clientID)
        {
            var addNameList = _dbContext.AdditionalNames.Where( x => x.Product.ClientID == clientID).ToList();
            if (addNameList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };

            return new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addNameList };
        }

        public Response GetNamesByID(int ID, int clientID)
        {
            var addNameList = _dbContext.AdditionalNames.Where(x => x.ID == ID && x.Product.ClientID == clientID).ToList();
            if (addNameList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };

            return new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addNameList };

        }
        /// <summary>
        /// Funkcja pokazuje dodatkowe nazwy których tytuł zawiera podane słowo
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Title"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Response GetNamesByIDNames(int ID, string Title, int clientID)
        {
            var addNameList = _dbContext.AdditionalNames.Where(x => x.ID == ID && x.Title.Contains(Title) && x.Product.ClientID == clientID).ToList();
            if (addNameList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };

            return new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addNameList };

        }

        public Response GetNamesByIDRegion(int ID, int Region, int clientID)
        {
            var addNameList = _dbContext.AdditionalNames.Where(x => x.ID == ID && x.RegionID == Region && x.Product.ClientID == clientID).ToList();
            if (addNameList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };

            return new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addNameList };

        }
        /// <summary>
        /// Funkcja pokazuje dodatkowe nazwy wybranego produktu
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>

        public Response GetAllNamesInProduct(int clientID, int productID)
        {

            var addNameList = _dbContext.AdditionalNames.Where(x => x.ProductID == productID).ToList();
            if (addNameList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };

            return new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addNameList };
        }

        #endregion

        #region Edit
        public Response EditAdditionalName(int clientID, int ID, AdditionalNameDto data)
        {
            var findName = _dbContext.AdditionalNames.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);
            if (findName == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };

            findName.Title = data.Title;
            findName.Value = data.Value;
            _dbContext.SaveChanges();

            return new AdditionalNameEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, additionalNameEdit = findName };
        }
        #endregion

        #region Delete
        public Response DeleteAdditionalName(int clientID, int ID)
        {
            var findAdditionalName = _dbContext.AdditionalNames.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);

            if (findAdditionalName == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };

            _dbContext.AdditionalNames.Remove(findAdditionalName);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };
        }
        #endregion
    }
}
