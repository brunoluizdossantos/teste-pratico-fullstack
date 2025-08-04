using Domain.Utils;
using FluentValidation;

namespace Application.Utils.Validations.CustomValidations;

public class IdValidator : AbstractValidator<Guid>
{
	public IdValidator()
	{
		RuleFor(x => x)
			.NotEmpty()
			.WithMessage(Constants.IdRequiredError);
	}
}
