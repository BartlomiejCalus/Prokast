using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.DictionaryParamsResponseModels;
using Prokast.Server.Services.Interfaces;


namespace Prokast.Server.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public DictionaryService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Get
        public Response GetAllParams()
        {
            var paramList = _dbContext.DictionaryParams.ToList();
            var response = new DictionaryGetResponse() { ID = random.Next(1, 100000), Model = paramList };
            if (paramList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000),  errorMsg = "Brak parametrów" };
                return responseNull;
            }
            return response;
        }
        
        public Response GetParamsByID( int ID)
        {
            var param = _dbContext.DictionaryParams.Where(x => x.ID == ID).ToList();
            var response = new DictionaryGetResponse() { ID = random.Next(1, 100000),  Model = param};
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000),  errorMsg = "Nie ma takiego parametru" };
                return responseNull;
            }
            return response;

        }

        public Response GetParamsByName( string name)
        {
            var param = _dbContext.DictionaryParams.Where(x => x.Name == name).ToList();


            var response = new DictionaryGetResponse() { ID = random.Next(1, 100000), Model = param };
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma modelu z taką nazwą" };
                return responseNull;
            }
            return response;
        }

        public Response GetParamsByRegion (int region)
        {
            var param = _dbContext.DictionaryParams.Where(x => x.IdRegion == region).ToList();

            List<string> paramList = new List<string>();
            foreach ( var x in param)
            {
                if (!paramList.Contains(x.Name))
                {
                    paramList.Add(x.Name);
                }
                
            }

            var response = new DictionaryUniqueGetResponse() { ID = random.Next(1, 100000), Model = paramList };
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego regionu" };
                return responseNull;
            }
            return response;
        }

        public Response GetValuesByName (string name)
        {
            var param = _dbContext.DictionaryParams.Where(x => x.Name == name).ToList();

            List<string> paramList = new List<string>();
            foreach( var x in param)
            {
                paramList.Add(x.Value);
            }
            var response = new DictionaryUniqueGetResponse() { ID = random.Next(1, 100000), Model = paramList };
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Błędna nazwa" };
                return responseNull;
            }
            return response;
        }
        #endregion

    }
}
