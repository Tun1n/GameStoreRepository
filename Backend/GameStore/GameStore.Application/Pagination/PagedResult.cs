namespace GameStore.Application.Pagination
{
   public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasNext => Page < TotalPages;
        public bool HasPrevious => Page > 1;
    }
}
    