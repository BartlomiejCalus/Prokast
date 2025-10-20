namespace Prokast.Server.Models
{
    public static class PaginationExtension
    {
        public static Paginated<T> Paginate<T>(IQueryable<T> sourceList, int pageNumber, int pageSize)
        {
            var totalItems = sourceList.Count();

            var items = sourceList.Skip((pageNumber - 1) *pageSize).Take(pageSize).ToList();

            return new Paginated<T>
            {
                TotalItems = totalItems,
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
