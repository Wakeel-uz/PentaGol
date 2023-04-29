namespace PentaGol.Domain.Configurations
{
    // Represents metadata for pagination results, including the current page, total pages, and total count of items.
    public class PaginationMetaData
    {
        // The current page number.
        public int CurrentPage { get; set; }

        // The total number of pages based on the page size and the total count of items.
        public int TotalPages { get; set; }

        // The total count of items.
        public int TotalCount { get; set; }

        // Indicates whether there is a previous page.
        public bool HasPrevious => CurrentPage > 1;

        // Indicates whether there is a next page.
        public bool HasNext => CurrentPage < TotalPages;

        // Initializes a new instance of the PaginationMetaData class with the specified total count and pagination parameters.
        public PaginationMetaData(int totalCount, PaginationParams @params)
        {
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)@params.PageSize);
            CurrentPage = @params.PageIndex;
        }
    }
}
