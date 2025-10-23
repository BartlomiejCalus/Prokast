namespace Prokast.Server.Models.ResponseModels
{
    public class PaginationResponse<T>: Response
    {
        public Paginated<T> Model { get; set; }
    }
}
