namespace Prokast.Server.Models
{
    public class Paginated<T>
    {
        public List<T> Items { get; set; } = [];
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
