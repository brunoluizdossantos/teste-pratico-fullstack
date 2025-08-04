using Application.Utils.Filters;
using Application.Utils.Validations.CustomValidations;
using Domain.Utils;
using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Application.DTOs;

public class UserCreateDto
{
	[SwaggerSchema(Description = Constants.UserSchemaName)]
	[SwaggerSchemaExample(Constants.UserSchemaNameValue)]
	public string Name { get; set; } = string.Empty;

	[SwaggerSchema(Constants.UserSchemaEmail)]
	[SwaggerSchemaExample(Constants.UserSchemaEmailValue)]
	public string Email { get; set; } = string.Empty;

	[JsonIgnore]
	public string HashedPassword { get; set; } = string.Empty;

	[JsonIgnore]
	public string Salt { get; set; } = string.Empty;
}

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
	public UserCreateDtoValidator()
	{
		RuleFor(x => x.Name).SetValidator(new NameValidator());
		RuleFor(x => x.Email).SetValidator(new EmailValidator());
	}
}