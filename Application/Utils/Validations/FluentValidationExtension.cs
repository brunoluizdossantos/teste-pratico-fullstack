using FluentValidation.Results;

namespace Application.Utils.Validations;

public static class FluentValidationExtension
{
	public static List<MessageResult>? GetErrors(this ValidationResult result)
	{
		return result.Errors?.Select(error => new MessageResult(error.ErrorMessage)).ToList();
	}

	public class MessageResult
	{
		public string Message { get; private set; } = string.Empty;

		public MessageResult(string message)
		{
			Message = message;
		}
	}
}