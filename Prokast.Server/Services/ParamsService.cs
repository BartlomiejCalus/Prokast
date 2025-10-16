using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Models.ResponseModels.DictionaryParamsResponseModels;
using Prokast.Server.Services.Interfaces;


namespace Prokast.Server.Services
{
    public class ParamsService : IParamsService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();
        
        public ParamsService(ProkastServerDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Create
        public Response CreateCustomParam([FromBody] CustomParamsDto customParamsDto, int clientID, int regionID, int productID) 
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (customParamsDto == null)
            {
                return responseNull;
            }

            var product = _dbContext.Products.Include(p => p.CustomParams).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
            {
                responseNull.errorMsg = "Nie ma takiego produktu!";
                return responseNull;
            }

            var customParam = new CustomParams
            {
                Name = customParamsDto.Name.ToString(),
                Type = customParamsDto.Type.ToString(),
                Value = customParamsDto.Value.ToString(),
                RegionID = regionID,
                Product = product
            };
            
            product.CustomParams.Add(customParam);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1,100000), ClientID = clientID};
            return response;
        }
        #endregion

        #region Get
        public Response GetAllParams(int clientID)
        {
            var paramList = _dbContext.CustomParams.Where(x => x.Product.ClientID == clientID).ToList();
            if(paramList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów" };
                return responseNull;
            }

            var response = new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = paramList };
            return response;
        }
        

        
        public Response GetParamsByID(int clientID, int ID)
        {
            var param = _dbContext.CustomParams.Where(x => x.Product.ClientID == clientID && x.ID == ID).ToList();
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }

            var response = new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
            return response;

        }
        

        
        public Response GetParamsByName(int clientID, string name) 
        {
            var param = _dbContext.CustomParams.Where(x => x.Product.ClientID == clientID && x.Name.Contains(name)).ToList();
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma modelu z taką nazwą" };
                return responseNull;
            }
            var response = new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
            return response;
        }

        public Response GetAllParamsInProduct(int clientID, int productID)
        {

            var paramList = _dbContext.CustomParams.Where(x => x.ProductID == productID).ToList();
            if (paramList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Produkt nie ma tych parametrów!" };
                return responseNull;
            }

            var response = new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = paramList };
            return response;
        }
        #endregion

        #region Edit
        public Response EditParams(int clientID, int ID, CustomParamsDto data)
        {
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);
            

            if (findParam == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            findParam.Name = data.Name;
            findParam.Type = data.Type;
            findParam.Value = data.Value;
            _dbContext.SaveChanges();

            var response = new ParamsEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, customParams = findParam };
            
            return response;
        }
        #endregion

        #region Delete
        public Response DeleteParams(int clientID, int ID)
        {
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findParam == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            _dbContext.Remove(findParam);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };
            return response;
        }
        #endregion
    }
}
