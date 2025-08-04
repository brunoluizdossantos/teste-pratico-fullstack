using Application.Utils.Filters;
using Application.Utils.Validations.CustomValidations;
using Domain.Utils;
using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Application.DTOs;

public class UserUpdateDto : UserCreateDto
{
	[SwaggerSchema(Description = Constants.UserIdProperty)]
	[SwaggerSchemaExample(Constants.DefaultSchemaIdValue)]
	[JsonIgnore]
	public Guid Id { get; set; }
}

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
	public UserUpdateDtoValidator()
	{
		RuleFor(x => x.Id).SetValidator(new IdValidator());
		RuleFor(x => x.Name).SetValidator(new NameValidator());
		RuleFor(x => x.Email).SetValidator(new EmailValidator());
	}
}