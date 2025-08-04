using Application.DTOs;
using Application.Interfaces;
using Application.Mediator.Users.Commands;
using Application.Mediator.Users.Queries;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Pagination;
using Domain.Utils;
using FluentAssertions;
using MediatR;
using Moq;
using System.Security.Cryptography;

namespace Tests.Integration.Service;

public class UserServiceTest
{
	private readonly Mock<IMapper> _mapperMock = new();
	private readonly Mock<IMediator> _mediatorMock = new();
	private readonly Mock<IUtilService> _utilServiceMock = new();
	private readonly UserService _service;
	private readonly User _object = new(Constants.UserSchemaNameValue, Constants.UserSchemaEmailValue, Constants.UserSchemaHashedPassword, Constants.UserSchemaSalt);

	public UserServiceTest()
	{
		_service = new UserService(_mapperMock.Object, _mediatorMock.Object, _utilServiceMock.Object);
	}

	[Fact(DisplayName = Constants.UserServiceTestGetAllItems)]
	public async Task GetAllItems_ShouldReturnMappedList()
	{
		// Arrange
		var query = new GetAllUsersQuery(
			int.Parse(Constants.RequestSchemaPageNumberValue),
			int.Parse(Constants.RequestSchemaPageSizeValue));

		var result = new PagedResponse<User>(
			new List<User> { _object },
			int.Parse(Constants.RequestSchemaPageNumberValue),
			int.Parse(Constants.RequestSchemaPageSizeValue),
			int.Parse(Constants.RequestSchemaTotalRecordsValue));

		var userDto = new UserDto
		{
			Id = _object.Id,
			Name = _object.Name,
			Email = _object.Email
		};

		var expected = new PagedResponseDto<UserDto>
		{
			PageNumber = int.Parse(Constants.RequestSchemaPageNumberValue),
			PageSize = int.Parse(Constants.RequestSchemaPageSizeValue),
			TotalPages = int.Parse(Constants.RequestSchemaTotalPagesValue),
			TotalRecords = int.Parse(Constants.RequestSchemaTotalRecordsValue),
			Data = new List<UserDto> { userDto }
		};

		_mediatorMock
			.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(result);

		_mapperMock
			.Setup(m => m.Map<PagedResponseDto<UserDto>>(result))
			.Returns(expected);

		var requestDto = new PagedRequestDto
		{
			PageNumber = int.Parse(Constants.RequestSchemaPageNumberValue),
			PageSize = int.Parse(Constants.RequestSchemaPageSizeValue)
		};

		// Act
		var response = await _service.GetAllUsers(requestDto);

		// Assert
		response.Should().BeEquivalentTo(expected);

		// Optional verifications
		_mediatorMock.Verify(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
		_mapperMock.Verify(m => m.Map<PagedResponseDto<UserDto>>(It.IsAny<PagedResponse<User>>()), Times.Once);
	}

	[Fact(DisplayName = Constants.UserServiceTestGetById)]
	public async Task GetById_ShouldReturnMappedDto()
	{
		// Arrange
		Guid id = Guid.NewGuid();
		var query = new GetUserByIdQuery(id);
		var domainResult = _object;
		var expected = new UserDto { Id = _object.Id, Name = _object.Name, Email = _object.Email };

		_mediatorMock.Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.Id == id), It.IsAny<CancellationToken>())).ReturnsAsync(domainResult);
		_mapperMock.Setup(m => m.Map<UserDto>(domainResult)).Returns(expected);

		// Act
		var result = await _service.GetUserById(id);

		// Assert
		result.Should().Be(expected);
	}

	[Fact(DisplayName = Constants.UserServiceTestCreate)]
	public async Task Create_ShouldReturnMappedDto()
	{
		// Arrange
		var createDto = new UserCreateDto { Name = _object.Name, Email = _object.Email };
		var command = new UserCreateCommand();
		var domainResult = _object;
		var expected = new UserResponseDto { Id = _object.Id, Name = _object.Name, Email = _object.Email, HashedPassword = _object.HashedPassword, Salt = _object.Salt };
		var randomPassword = RandomNumberGenerator.GetString(Constants.TestAllowedChars, Constants.DefaultPasswordLength);
		var expectedPass = new GeneratePasswordDto { Password = randomPassword, Hash = _object.HashedPassword, Salt = _object.Salt };
		
		_mapperMock.Setup(m => m.Map<UserCreateCommand>(createDto)).Returns(command);
		_mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(domainResult);
		_mapperMock.Setup(m => m.Map<UserResponseDto>(domainResult)).Returns(expected);
		_utilServiceMock.Setup(u => u.GeneratePassword()).Returns(expectedPass);

		// Act
		var result = await _service.CreateUser(createDto);

		// Assert
		result.Should().Be(expected);
	}

	[Fact(DisplayName = Constants.UserServiceTestUpdate)]
	public async Task Update_ShouldReturnMappedDto()
	{
		// Arrange
		var dto = new UserUpdateDto { Id = _object.Id, Name = _object.Name, Email = _object.Email };
		var command = new UserUpdateCommand();
		var result = _object;
		var expected = new UserDto { Id = _object.Id, Name = _object.Name, Email = _object.Email };

		_mapperMock.Setup(m => m.Map<UserUpdateCommand>(dto)).Returns(command);
		_mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);
		_mapperMock.Setup(m => m.Map<UserDto>(result)).Returns(expected);

		// Act
		var response = await _service.UpdateUser(dto);

		// Assert
		response.Should().Be(expected);
	}
}