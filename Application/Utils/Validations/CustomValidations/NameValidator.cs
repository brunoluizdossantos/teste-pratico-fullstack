using Domain.Utils;
using FluentValidation;

namespace Application.Utils.Validations.CustomValidations;

public class EmailValidator : AbstractValidator<string>
{
	public EmailValidator()
	{
		RuleFor(x => x)
			.NotEmpty()
			.WithMessage(Constants.EmailRequiredError);

		RuleFor(x => x)
			.MinimumLength(Constants.EmailMinLength)
			.WithMessage(Constants.EmailMinLengthError);

		RuleFor(x => x)
			.MaximumLength(Constants.EmailMaxLength)
			.WithMessage(Constants.EmailMaxLengthError);
	}
}
