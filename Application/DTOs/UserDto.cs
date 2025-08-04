using Application.Utils.Filters;
using Domain.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs;

public class UserDto : EntityDto
{
	[SwaggerSchema(Description = Constants.UserSchemaName)]
	[SwaggerSchemaExample(Constants.UserSchemaNameValue)]
	public string Name { get; set; } = string.Empty;

	[SwaggerSchema(Description = Constants.UserSchemaEmail)]
	[SwaggerSchemaExample(Constants.UserSchemaEmailValue)]
	public string Email { get; set; } = string.Empty;

	[JsonIgnore]
	public string HashedPassword { get; set; } = string.Empty;

	[JsonIgnore]
	public string Salt { get; set; } = string.Empty;

	[DataType(DataType.Date)]
	public DateTime CreatedAt { get; protected set; }
}
