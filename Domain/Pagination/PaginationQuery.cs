namespace Domain.Pagination
{
    public class PaginationQuery
    {
        public string? Query { get; set; }


        private int? _page;
        public int Page
        {
            get => _page ?? 1;
            set => _page = (value <= 0) ? null : value;
        }

        private int? _pageSize;
        public int PageSize
        {
            get => _pageSize ?? int.MaxValue;
            set => _pageSize = (value <= 0) ? null : value;
        }
    }
}
