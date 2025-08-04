using Application.Utils.Filters;
using Domain.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs;

public abstract class EntityDto
{
	[SwaggerSchema(Description = Constants.IdProperty)]
	[SwaggerSchemaExample(Constants.DefaultSchemaIdValue)]
	public Guid Id { get; set; }
}