namespace PentaGol.Domain.Configurations
{
    // Represents a pagination configuration with a maximum page size of 15.
    public class PaginationParams
    {
        private const int _maxPageSize = 15; // The maximum number of items that can be displayed per page.
        private int _pageSize; // The actual number of items per page.

        // Gets or sets the number of items to display per page, capped at the maximum page size.
        public int PageSize
        {
            get => _pageSize; // Returns the actual number of items per page.
            set => _pageSize = value > _maxPageSize ? _maxPageSize : value; // Sets the actual number of items per page, capped at the maximum page size.
        }

        // Gets or sets the current page number.
        public int PageIndex { get; set; } // The current page number, assumed to be within the range of available pages.
    }
}
