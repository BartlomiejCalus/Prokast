using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Services
{
    public class OthersService: IOthersService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public OthersService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public Response GetRegions()
        {
            var regions = _dbContext.Regions.ToList();
            if (regions.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Klient nie ma parametrów" };
                return responseNull;
            }

            var response = new RegionsResponse() { ID = random.Next(1, 100000), ClientID = -1, Model = regions };
            return response;
        }
    }
}
