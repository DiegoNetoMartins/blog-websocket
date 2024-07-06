namespace Blog.Shared.Responses;

public class PaginatedListResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public List<T> Data { get; set; }

    public PaginatedListResponse(int pageNumber, int pageSize, int totalPages, int totalCount, List<T> data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalCount = totalCount;
        Data = data;
    }
}
