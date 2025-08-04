using Application.Utils.Filters;
using Domain.Utils;
using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs;

public record PagedRequestDto
{
	[SwaggerSchema(Description = Constants.PageNumberProperty)]
	[SwaggerSchemaExample(Constants.RequestSchemaPageNumberValue)]
	public int PageNumber { get; set; }

	[SwaggerSchema(Description = Constants.PageSizeProperty)]
	[SwaggerSchemaExample(Constants.RequestSchemaPageSizeValue)]
	public int PageSize { get; set; }
}

public class PagedRequestDtoValidator : AbstractValidator<PagedRequestDto>
{
	public PagedRequestDtoValidator()
	{
		RuleFor(x => x.PageNumber)
			.GreaterThanOrEqualTo(Constants.PageNumberMinValue)
			.WithMessage(Constants.PageNumberMinValueError);

		RuleFor(x => x.PageSize)
			.GreaterThanOrEqualTo(Constants.PageSizeMinValue)
			.WithMessage(Constants.PageSizeMinValueError);
	}
}
