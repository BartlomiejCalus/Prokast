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
        public Response CreateCustomParam([FromBody] CustomParamsDto customParamsDto, int clientID, int productID) 
        {
            if (customParamsDto == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var product = _dbContext.Products.Include(p => p.CustomParams).FirstOrDefault(x => x.ID == productID && x.ClientID == clientID);
            if (product == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            var customParam = new CustomParams
            {
                Name = customParamsDto.Name.ToString(),
                Type = customParamsDto.Type.ToString(),
                Value = customParamsDto.Value.ToString(),
                RegionID = customParamsDto.RegionID,
                Product = product
            };
            
            product.CustomParams.Add(customParam);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }
        #endregion

        #region Get
        public Response GetAllParams(int clientID)
        {
            var paramList = _dbContext.CustomParams.Where(x => x.Product.ClientID == clientID).ToList();
            if(paramList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów" };

            return new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = paramList };
        }
        

        
        public Response GetParamsByID(int clientID, int ID)
        {
            var param = _dbContext.CustomParams.Where(x => x.Product.ClientID == clientID && x.ID == ID).ToList();
            if (param.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego parametru" };

            return new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };

        }
        

        
        public Response GetParamsByName(int clientID, string name) 
        {
            var param = _dbContext.CustomParams.Where(x => x.Product.ClientID == clientID && x.Name.Contains(name)).ToList();
            if (param.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma modelu z taką nazwą" };

            return new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
        }

        public Response GetAllParamsInProduct(int clientID, int productID)
        {

            var paramList = _dbContext.CustomParams.Where(x => x.ProductID == productID).ToList();
            if (paramList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Produkt nie ma tych parametrów!" };

            return new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = paramList };
        }
        #endregion

        #region Edit
        public Response EditParams(int clientID, int ID, CustomParamsDto data)
        {
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);
            
            if (findParam == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };

            findParam.Name = data.Name;
            findParam.Type = data.Type;
            findParam.Value = data.Value;
            _dbContext.SaveChanges();

            return new ParamsEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, customParams = findParam };
        }
        #endregion

        #region Delete
        public Response DeleteParams(int clientID, int ID)
        {
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.Product.ClientID == clientID && x.ID == ID);


            if (findParam == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };

            _dbContext.Remove(findParam);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Parametr został usumięty" };
        }
        #endregion
    }
}
