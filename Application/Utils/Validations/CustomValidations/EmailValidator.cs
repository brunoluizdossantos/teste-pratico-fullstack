using Domain.Utils;
using FluentValidation;

namespace Application.Utils.Validations.CustomValidations;

public class NameValidator : AbstractValidator<string>
{
	public NameValidator()
	{
		RuleFor(x => x)
			.NotEmpty()
			.WithMessage(Constants.NameRequiredError);

		RuleFor(x => x)
			.MinimumLength(Constants.NameMinLength)
			.WithMessage(Constants.NameMinLengthError);

		RuleFor(x => x)
			.MaximumLength(Constants.NameMaxLength)
			.WithMessage(Constants.NameMaxLengthError);
	}
}
