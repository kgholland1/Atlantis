using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Helpers
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string PreviousPage { get; set; }
        public string NextPage { get; set; }

        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems,
            int totalPages, string previousPage, string nextPage)
        {
            this.CurrentPage = currentPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
            this.PreviousPage = previousPage;
            this.NextPage = nextPage;
        }
    }
}
