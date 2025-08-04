using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Utils;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

public class RegisterControllerTests
{
	private readonly Mock<IValidator<UserCreateDto>> _validatorMock;
	private readonly Mock<IUserService> _userServiceMock;
	private readonly RegisterController _controller;
	private readonly User _object = new(Constants.UserSchemaNameValue, Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);

	public RegisterControllerTests()
	{
		_validatorMock = new Mock<IValidator<UserCreateDto>>();
		_userServiceMock = new Mock<IUserService>();
		_controller = new RegisterController(_validatorMock.Object, _userServiceMock.Object);
	}

	[Fact(DisplayName = Constants.UserServiceTestCreate)]
	public async Task CreateUser_ReturnsOk_WhenValidDtoAndServiceSucceeds()
	{
		// Arrange
		var dto = new UserCreateDto { Name = _object.Name, Email = _object.Email };
		var expectedResponse = new UserResponseDto { Id = Guid.NewGuid(), Name = _object.Name, Email = _object.Email };

		_validatorMock
			.Setup(v => v.ValidateAsync(dto, default))
			.ReturnsAsync(new ValidationResult());

		_userServiceMock
			.Setup(s => s.CreateUser(dto))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _controller.CreateUser(dto);

		// Assert
		var ok = Assert.IsType<ActionResult<UserResponseDto>>(result);
		var value = Assert.IsType<OkObjectResult>(ok.Result);
		Assert.Equal(StatusCodes.Status200OK, value.StatusCode);

		var returned = Assert.IsType<UserResponseDto>(value.Value);
		Assert.Same(expectedResponse, returned);
	}
}
