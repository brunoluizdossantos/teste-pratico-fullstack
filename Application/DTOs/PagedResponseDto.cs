using Application.Utils.Filters;
using Domain.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs;

public class PagedResponseDto<T>
{
	[SwaggerSchema(Description = Constants.PageNumberProperty)]
	[SwaggerSchemaExample(Constants.RequestSchemaPageNumberValue)]
	public int PageNumber { get; init; }

	[SwaggerSchema(Description = Constants.PageSizeProperty)]
	[SwaggerSchemaExample(Constants.RequestSchemaPageSizeValue)]
	public int PageSize { get; init; }

	[SwaggerSchema(Description = Constants.TotalPagesProperty)]
	[SwaggerSchemaExample(Constants.RequestSchemaTotalPagesValue)]
	public int TotalPages { get; init; }

	[SwaggerSchema(Description = Constants.TotalRecordsProperty)]
	[SwaggerSchemaExample(Constants.RequestSchemaTotalRecordsValue)]
	public int TotalRecords { get; init; }

	public List<T> Data { get; init; } = new List<T>();
}