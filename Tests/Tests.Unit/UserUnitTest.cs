using Domain.Entities;
using Domain.Utils;
using Domain.Validation;
using FluentAssertions;
using System.Security.Cryptography;

namespace Tests.Unit;

public class UserUnitTest
{
	[Fact(DisplayName = Constants.UserUnitTestCreateWithValidParameters)]
	public void Create_WithValidParameters_ResultObjectValidState()
	{
		Action action = () => new User(Constants.UserSchemaNameValue, Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().NotThrow<DomainExceptionValidation>();
	}

	[Fact(DisplayName = Constants.UserUnitTestValidShortNameValue)]
	public void Valid_ShortNameValue_DomainException()
	{
		Action action = () => new User(RandomNumberGenerator.GetString(Constants.TestAllowedChars, Constants.NameMinLength - 1), Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>()
			.WithMessage(Constants.NameMinLengthError);
	}

	[Fact(DisplayName = Constants.UserUnitTestValidLongNameValue)]
	public void Valid_LongNameValue_DomainException()
	{
		Action action = () => new User(RandomNumberGenerator.GetString(Constants.TestAllowedChars, Constants.NameMaxLength + 1), Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>()
			.WithMessage(Constants.NameMaxLengthError);
	}

	[Fact(DisplayName = Constants.UserUnitTestValidMissingNameValue)]
	public void Valid_MissingNameValue_DomainException()
	{
		Action action = () => new User("", Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>()
			.WithMessage(Constants.NameRequiredError);
	}

	[Fact(DisplayName = Constants.UserUnitTestValidNullNameValue)]
	public void Valid_NullNameValue_DomainException()
	{
		Action action = () => new User(null, Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>();
	}

	[Fact(DisplayName = Constants.UserUnitTestValidShortEmailValue)]
	public void Valid_ShortEmailValue_DomainException()
	{
		Action action = () => new User(Constants.UserSchemaNameValue, RandomNumberGenerator.GetString(Constants.TestAllowedChars, Constants.EmailMinLength - 1), Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>()
			.WithMessage(Constants.EmailMinLengthError);
	}

	[Fact(DisplayName = Constants.UserUnitTestValidLongEmailValue)]
	public void Valid_LongEmailValue_DomainException()
	{
		Action action = () => new User(Constants.UserSchemaNameValue, RandomNumberGenerator.GetString(Constants.TestAllowedChars, Constants.EmailMaxLength + 1), Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>()
			.WithMessage(Constants.EmailMaxLengthError);
	}

	[Fact(DisplayName = Constants.UserUnitTestValidMissingEmailValue)]
	public void Valid_MissingEmailValue_DomainException()
	{
		Action action = () => new User(Constants.UserSchemaNameValue, "", Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>()
			.WithMessage(Constants.EmailRequiredError);
	}

	[Fact(DisplayName = Constants.UserUnitTestValidNullEmailValue)]
	public void Valid_NullEmailValue_DomainException()
	{
		Action action = () => new User(Constants.UserSchemaNameValue, null, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);
		action.Should().Throw<DomainExceptionValidation>();
	}
}