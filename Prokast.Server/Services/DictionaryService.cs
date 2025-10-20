using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels;
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
            if (paramList.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000),  errorMsg = "Brak parametrów" };
                return responseNull;
            }

            var response = new DictionaryGetResponse() { ID = random.Next(1, 100000), Model = paramList };
            return response;
        }
        
        public Response GetParamsByID(int ID)
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

        public Response GetParamsByName(string name)
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

        public Response GetParamsByRegion (int regionID)
        {
            var param = _dbContext.DictionaryParams.Where(x => x.RegionID == regionID).ToList();

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
        /// <summary>
        /// Funkcja pokazuje wartości wybranego parametru słownikowego
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Response GetValuesByName (string name)
        {
            var param = _dbContext.DictionaryParams.Where(x => x.Name == name).ToList();
            if (param.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Błędna nazwa" };
                return responseNull;
            }
            //var paginatedResult = PaginationExtension.Paginate(param, 2, 5);

            List<string> paramList = new List<string>();
            foreach( var x in param)
            {
                paramList.Add(x.Value);
            }

            var response = new DictionaryUniqueGetResponse() { ID = random.Next(1, 100000), Model = paramList };

            //var response = new PaginationTestResponse<DictionaryParams>() { ID = random.Next(1, 100000), Model = paginatedResult };

            return response;
        }

        //TODO: do poprawy
        //public Response GetAllParamsInProduct(int clientID, int productID)
        //{
        //    var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Nie ma takiego parametru" };

        //    var product = _dbContext.Products.FirstOrDefault(x => x.ClientID == clientID && x.ID == productID);
        //    if (product == null)
        //    {
        //        responseNull.errorMsg = "Nie ma takiego produktu!";
        //        return responseNull;
        //    }
        //    var dictionaryParamsIDList = product.DictionaryParams.Split(",")
        //                      .Select(x => int.Parse(x)).ToList();

        //    var dictionaryParamsList = _dbContext.DictionaryParams.Where(x => dictionaryParamsIDList.Contains(x.ID)).ToList();
        //    if (dictionaryParamsList.Count() == 0)
        //    {
        //        return responseNull;
        //    }

        //    var response = new DictionaryGetResponse() { ID = random.Next(1, 100000), Model = dictionaryParamsList };
        //    return response;

        //}

        #endregion

    }
}
