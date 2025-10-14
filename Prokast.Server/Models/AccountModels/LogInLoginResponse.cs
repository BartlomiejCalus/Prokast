namespace Prokast.Server.Models.ResponseModels.AccountResponseModels
{
    public class LogInLoginResponse : Response
    {
        public bool IsSubscribed { get; set; }
        public string Token { get; set; }
    }
}
