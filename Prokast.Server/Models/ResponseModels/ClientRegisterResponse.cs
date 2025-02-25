using Prokast.Server.Entities;
using Prokast.Server.Models.ClientModels;

namespace Prokast.Server.Models.ResponseModels
{
    public class ClientRegisterResponse: Response
    {
        public Registration Registration { get; set; }
    }
}
