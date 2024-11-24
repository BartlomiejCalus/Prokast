using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;


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

        #region CreateCustonParam
        public Response CreateCustomParam([FromBody] CustomParamsDto customParamsDto, int clientID ) 
        {
            
            var param = _mapper.Map<CustomParamsDto>(customParamsDto);
            if (param == null)
            {
                var responseNull = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = "Błędnie podane dane" };
                return responseNull;
            }

            var customParam = new CustomParams
            {
                Name = param.Name.ToString(),
                Type = param.Type.ToString(),
                Value = param.Value.ToString(),
                ClientID = clientID
            };
            

            _dbContext.CustomParams.Add(customParam);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1,100000), ClientID = clientID, Model = param };
            return response;
        }
        #endregion

        #region GetAllParams
        public Response GetAllParams(int clientID)
        {
            var paramList = _dbContext.CustomParams.Where(x => x.ClientID == clientID).ToList();
            var response = new Response() { ID = random.Next(1,100000), ClientID = clientID, Model = paramList };
            if(paramList.Count() == 0)
            {
                var responseNull = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = "Klient nie ma parametrów" };
                return responseNull;
            }
            return response;
        }
        #endregion

        #region GetParamsByID
        public Response GetParamsByID(int clientID, int ID)
        {
            var param = _dbContext.CustomParams.Where(x => x.ClientID == clientID && x.ID == ID);
            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
            if (param.Count() == 0)
            {
                var responseNull = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = "Nie ma takiego parametru" };
                return responseNull;
            }   
            return response;

        }
        #endregion

        #region GetParamsByName
        public Response GetParamsByName(int clientID, string name) 
        {
            var param = _dbContext.CustomParams.Where(x => x.ClientID == clientID && x.Name.Contains(name)).ToList();

            
            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = param };
            if (param.Count() == 0)
            {
                var responseNull = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = "Nie ma modelu z taką nazwą" };
                response = responseNull;
            }
            return response;
        }
        #endregion

        #region EditParams
        public Response EditParams(int clientID, int ID, CustomParamsDto data)
        {
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);
            

            if (findParam == null)
            {
                var responseNull = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = "Nie ma takiego modelu!" };
                return responseNull;
            }

            findParam.Name = data.Name;
            findParam.Type = data.Type;
            findParam.Value = data.Value;
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = findParam };
            
            return response;
        }
        #endregion

        #region DeleteParams
        public Response DeleteParams(int clientID, int ID)
        {
            var findParam = _dbContext.CustomParams.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);


            if (findParam == null)
            {
                var responseNull = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = "Nie ma takiego modelu!" };
                return responseNull;
            }

            _dbContext.Remove(findParam);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID, Model = "Parametr został usumięty" };

            return response;
        }
        #endregion
    }
}
