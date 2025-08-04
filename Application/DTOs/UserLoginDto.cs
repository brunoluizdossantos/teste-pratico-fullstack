using Application.Utils.Filters;
using Domain.Utils;
using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs;

public class UserLoginDto
{
	[SwaggerSchema(Description = Constants.EmailProperty)]
	[SwaggerSchemaExample(Constants.LoginSchemaEmailValue)]
	public string Email { get; set; } = string.Empty;

	[SwaggerSchema(Description = Constants.PasswordProperty)]
	[SwaggerSchemaExample(Constants.LoginSchemaPasswordValue)]
	public string Password { get; set; } = string.Empty;
}

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
	public UserLoginDtoValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.WithMessage(Constants.EmailRequiredError)
			.MinimumLength(Constants.EmailMinLength)
			.WithMessage(Constants.EmailMinLengthError)
			.MaximumLength(Constants.EmailMaxLength)
			.WithMessage(Constants.EmailMaxLengthError);

		RuleFor(x => x.Password)
			.NotEmpty()
			.WithMessage(Constants.PasswordRequiredError)
			.MinimumLength(Constants.PasswordMinLength)
			.WithMessage(Constants.PasswordMinLengthError)
			.MaximumLength(Constants.PasswordMaxLength)
			.WithMessage(Constants.PasswordMaxLengthError);
	}
}