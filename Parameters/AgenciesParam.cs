namespace AtlantisPortals.API.Parameters
{
    public class AgenciesParam
    {
        const int maxPageSize = 50;
        public string Ministry { get; set; }
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";
    }
}
