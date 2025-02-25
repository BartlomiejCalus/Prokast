using Prokast.Server.Entities;

namespace Prokast.Server.Models.ResponseModels.AccountResponseModels
{
    public class LogInGetResponse : Response
    {
        public List<AccountLogIn> Model { get; set; }
    }
}
