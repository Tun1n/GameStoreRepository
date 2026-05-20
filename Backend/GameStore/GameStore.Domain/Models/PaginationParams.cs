namespace GameStore.Domain.Models
{
    public class PaginationParams
    {
        const int maxPageSize = 26; 

        private int _pageNumber = 1;
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }

        private int _pageSize = 26;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 1 ? 1 : value > maxPageSize ? maxPageSize : value;
        }
    }
}
