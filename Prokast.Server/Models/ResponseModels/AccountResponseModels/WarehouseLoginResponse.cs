namespace Prokast.Server.Models.ResponseModels.AccountResponseModels
{
    public class WarehouseLoginResponse : Response

    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsSubscribed { get; set; }
        public string Token { get; set; }
        public int? WarehouseID { get; set; }
        
    }

}

