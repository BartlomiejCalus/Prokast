﻿using AutoMapper;
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
        public Response CreateAdditionalName([FromBody] AdditionalNameDto additionalNameDto, int clientID, int regionID, int productID)
        {    
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            if (additionalNameDto == null)
            {
                return responseNull;
            }

            var product = _dbContext.Products.Include(p => p.AdditionalNames).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
            {
                responseNull.errorMsg = "Nie ma takiego produktu!";
                return responseNull;
            }

            var additionalName = new AdditionalName
            {
                Title = additionalNameDto.Title.ToString(),
                Value = additionalNameDto.Value.ToString(),
                RegionID = regionID,
                Product = product
            };

            product.AdditionalNames.Add(additionalName);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllNames(int clientID)
        {
            var addNameList = _dbContext.AdditionalNames.Where( x => x.Product.ClientID == clientID).ToList();
            if (addNameList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Brak parametrów" };
                return responseNull;
            }

            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addNameList };
            return response;
        }

        public Response GetNamesByID(int ID, int clientID)
        {
            var addName = _dbContext.AdditionalNames.Where(x => x.ID == ID && x.Product.ClientID == clientID).ToList();
            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addName };
            if (addName.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

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
            var addName = _dbContext.AdditionalNames.Where(x => x.ID == ID && x.Title.Contains(Title) && x.Product.ClientID == clientID).ToList();
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
            var addName = _dbContext.AdditionalNames.Where(x => x.ID == ID && x.RegionID == Region && x.Product.ClientID == clientID).ToList();
            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), Model = addName };
            if (addName.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }
        /// <summary>
        /// Funkcja pokazuje dodatkowe nazwy wybranego produktu
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>

        public Response GetAllNamesInProduct(int clientID, int productID)
        {

            var names = _dbContext.AdditionalNames.Where(x => x.ProductID == productID).ToList();
            if (names.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Produkt nie ma tych parametrów!" };
                return responseNull;
            }

            var response = new AdditionalNameGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = names };
            return response;
        }

        #endregion

        #region Edit
        public Response EditAdditionalName(int clientID, int ID, AdditionalNameDto data)
        {
            var findName = _dbContext.AdditionalNames.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findName == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            findName.Title = data.Title;
            findName.Value = data.Value;
            _dbContext.SaveChanges();

            var response = new AdditionalNameEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, additionalNameEdit = findName };

            return response;
        }
        #endregion

        #region Delete
        //TODO: do poprawy
        public Response DeleteAdditionalName(int clientID, int ID)
        {
            var findAdditionalName = _dbContext.AdditionalNames.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findAdditionalName == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            _dbContext.AdditionalNames.Remove(findAdditionalName);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };

            return response;
            return null;
        }
        #endregion
    }
}
