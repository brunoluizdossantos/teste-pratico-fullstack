using Domain.Entities;
using Domain.Utils;
using FluentAssertions;
using Infra.Data.Repositories;

namespace Tests.Integration.Repository;

public class UserRepositoryTest
{
	private readonly User _objectCreate = new(Constants.UserSchemaNameValue, Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);

	[Fact(DisplayName = Constants.UserRepositoryTestCreate)]
	public async Task Create_ShouldPersistObject()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		var repository = new UserRepository(context);

		// Act
		await repository.CreateUserAsync(_objectCreate);
		await context.SaveChangesAsync();

		// Assert
		var list = context.User.ToList();
		list.Should().HaveCount(1);
		list[0].Name.Should().Be(Constants.UserSchemaNameValue);
		list[0].Email.Should().Be(Constants.UserSchemaEmailValue);

		TestDbContextFactory.Destroy(context);
	}

	[Fact(DisplayName = Constants.UserRepositoryTestGetAllItems)]
	public async Task GetAllItems_ShouldReturnOrderedList()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		var repository = new UserRepository(context);

		// Act
		await repository.CreateUserAsync(_objectCreate);
		await repository.CreateUserAsync(new($"{_objectCreate.Name} 2", $"dois.{_objectCreate.Email}", Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt));

		var list = (await repository.GetAllUsersAsync(int.Parse(Constants.RequestSchemaPageNumberValue), int.Parse(Constants.RequestSchemaPageSizeValue)));

		// Assert
		list.Data.Count.Should().Be(2);

		list.Data.First().Name.Should().Be(Constants.UserSchemaNameValue);
		list.Data.First().Email.Should().Be(Constants.UserSchemaEmailValue);

		list.Data.Last().Name.Should().Be($"{Constants.UserSchemaNameValue} 2");
		list.Data.Last().Email.Should().Be($"dois.{Constants.UserSchemaEmailValue}");

		TestDbContextFactory.Destroy(context);
	}

	[Fact(DisplayName = Constants.UserRepositoryTestGetById)]
	public async Task GetById_ShouldChangeValues()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		var repository = new UserRepository(context);

		// Act
		var created = await repository.CreateUserAsync(_objectCreate);
		created.Update($"{Constants.UserSchemaNameValue} atualizado", $"atualizado.{Constants.UserSchemaEmailValue}");
		await repository.UpdateUserAsync(created);

		// Assert
		var updated = await repository.GetUserByIdAsync(created.Id);
		updated.Name.Should().Be($"{Constants.UserSchemaNameValue} atualizado");
		updated.Email.Should().Be($"atualizado.{Constants.UserSchemaEmailValue}");

		TestDbContextFactory.Destroy(context);
	}
}