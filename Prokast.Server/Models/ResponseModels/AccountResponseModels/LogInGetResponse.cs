using Prokast.Server.Models.AccountModels;

namespace Prokast.Server.Models.ResponseModels.AccountResponseModels
{
    public class LogInGetResponse : Response
    {
        public List<AccountGetDto> Model { get; set; }
    }
}
