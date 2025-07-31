using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models.ClientModels;

namespace Prokast.Server.Models
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Registration, Clients>();
            CreateMap<Registration, Account>();
            
        }
    }
}
