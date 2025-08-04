namespace Domain.Pagination;

public class PagedResponse<T>
{
	public int PageNumber { get; init; }
	public int PageSize { get; init; }
	public int TotalPages { get; init; }
	public int TotalRecords { get; init; }
	public List<T> Data { get; init; }

	public PagedResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
	{
		Data = data;
		PageNumber = pageNumber;
		PageSize = pageSize;
		TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
		TotalRecords = totalRecords;
	}
}
