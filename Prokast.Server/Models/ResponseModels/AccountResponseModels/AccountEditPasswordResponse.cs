using Prokast.Server.Models.AccountModels;

namespace Prokast.Server.Models.ResponseModels.AccountResponseModels
{
    public class AccountEditPasswordResponse : Response
    {
        public AccountEditPasswordDto Model { get; set; }
    }
}
