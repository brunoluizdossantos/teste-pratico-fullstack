using Domain.Utils;
using Domain.Validation;

namespace Domain.Entities;

public sealed class User : Entity
{
	public string Name { get; private set; } = string.Empty;
	public string Email { get; private set; } = string.Empty;
	public string HashedPassword { get; private set; } = string.Empty;
	public string Salt { get; private set; } = string.Empty;

	public User(string name, string email, string hashedPassword, string salt)
	{
		CreatedAt = DateTime.Now;
		ValidateDomain(name, email, hashedPassword, salt);
	}

	public void Update(string name, string email)
	{
		UpdatedAt = DateTime.UtcNow;
		ValidateDomain(name, email, this.HashedPassword, this.Salt);
	}

	private void ValidateDomain(string name, string email, string hashedPassword, string salt)
	{
		DomainExceptionValidation.When(string.IsNullOrEmpty(name), Constants.NameRequiredError);
		DomainExceptionValidation.When(name.Length < Constants.NameMinLength, Constants.NameMinLengthError);
		DomainExceptionValidation.When(name.Length > Constants.NameMaxLength, Constants.NameMaxLengthError);

		DomainExceptionValidation.When(string.IsNullOrEmpty(email), Constants.EmailRequiredError);
		DomainExceptionValidation.When(email.Length < Constants.EmailMinLength, Constants.EmailMinLengthError);
		DomainExceptionValidation.When(email.Length > Constants.EmailMaxLength, Constants.EmailMaxLengthError);

		DomainExceptionValidation.When(string.IsNullOrEmpty(hashedPassword), Constants.HashRequiredError);
		DomainExceptionValidation.When(hashedPassword.Length < Constants.HashMinLength, Constants.HashMinLengthError);
		DomainExceptionValidation.When(hashedPassword.Length > Constants.HashMaxLength, Constants.HashMaxLengthError);

		DomainExceptionValidation.When(string.IsNullOrEmpty(salt), Constants.SaltRequiredError);
		DomainExceptionValidation.When(salt.Length < Constants.SaltMinLength, Constants.SaltMinLengthError);
		DomainExceptionValidation.When(salt.Length > Constants.SaltMaxLength, Constants.SaltMaxLengthError);

		Name = name;
		Email = email;
		HashedPassword = hashedPassword;
		Salt = salt;
	}
}
