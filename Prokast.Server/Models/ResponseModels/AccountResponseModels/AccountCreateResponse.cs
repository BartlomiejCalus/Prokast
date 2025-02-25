using Prokast.Server.Models.AccountModels;

namespace Prokast.Server.Models.ResponseModels.AccountResponseModels
{
    public class AccountCreateResponse : Response
    {
        public AccountCreateDto Model { get; set; }
    }
}
