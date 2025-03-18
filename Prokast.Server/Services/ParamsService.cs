using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
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
        public Response CreateCustomParam([FromBody] CustomParamsDto customParamsDto, int clientID ) 
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (customParamsDto == null)
            {
                return responseNull;
            }

            if (_dbContext.CustomParams.Any(x => x.Name == customParamsDto.Name))
            {
                responseNull.errorMsg = "Nazwa jest zajęta!";
                return responseNull;
            }

            var customParam = new CustomParams
            {
                Name = customParamsDto.Name,
                Type = customParamsDto.Type,
                Value = customParamsDto.Value,
                ClientID = clientID
            };
            

            _dbContext.CustomParams.Add(customParam);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1,100000), ClientID = clientID};
            return response;
        }
        #endregion

        #region Get
        public Response GetAllParams(int clientID)
        {
            var paramList = _dbContext.CustomParams.Where(x => x.ClientID == clientID).ToList();
            var response = new ParamsGetResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = paramList };
            if(paramList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów" };
                return responseNull;
            }
            return response;
        }
        

        
        public Response GetParamsByID(int clientID, int ID)
        {
            var param = _dbContext.CustomParams.Where(x => x.ClientID == clientID && x.ID == ID).ToList();
            var response = new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }   
            return response;

        }
        

        
        public Response GetParamsByName(int clientID, string name) 
        {
            var param = _dbContext.CustomParams.Where(x => x.ClientID == clientID && x.Name.Contains(name)).ToList();

            
            var response = new ParamsGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma modelu z taką nazwą" };
                return responseNull;
            }
            return response;
        }
        #endregion

        #region Edit
        public Response EditParams(int clientID, int ID, CustomParamsDto data)
        {
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);

            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
            if (findParam == null)
            {
                return responseNull;
            }

            if (_dbContext.CustomParams.Any(x => x.Name == data.Name))
            {
                responseNull.errorMsg = "Nazwa jest zajęta!";
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
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);


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
