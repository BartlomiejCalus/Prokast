namespace Prokast.Server.Models.ProductModels
{
    public class ProductGetFilter
    {
        public List<int>? ProductIDList { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ProductName { get; set; }
    }
}
