using Application.DTOs;
using Application.Interfaces;
using Application.Mediator.Users.Commands;
using Application.Mediator.Users.Queries;
using AutoMapper;
using Domain.Utils;
using MediatR;

namespace Application.Services;

public class UserService : IUserService
{
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;
	private readonly IUtilService _utilService;

	public UserService(IMapper mapper, IMediator mediator, IUtilService utilService)
	{
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		_utilService = utilService ?? throw new ArgumentNullException(nameof(utilService));
	}

	public async Task<UserResponseDto> CreateUser(UserCreateDto dto)
	{
		var userEmailExists = await this.GetUserByEmail(dto.Email);

		if (userEmailExists != null)
			throw new ArgumentException(Constants.UserEmailExists);

		var createdPassword = _utilService.GeneratePassword();

		dto.HashedPassword = createdPassword.Hash;
		dto.Salt = createdPassword.Salt;

		var command = _mapper.Map<UserCreateCommand>(dto);
		var result = await _mediator.Send(command);
		var userResponse = _mapper.Map<UserResponseDto>(result);

		userResponse.Password = createdPassword.Password;

		return userResponse;
	}

	public async Task<PagedResponseDto<UserDto>> GetAllUsers(PagedRequestDto dto)
	{
		var query = new GetAllUsersQuery(dto.PageNumber, dto.PageSize) ?? throw new NullReferenceException(Constants.EntityErrorNotLoaded);
		var result = await _mediator.Send(query);
		return _mapper.Map<PagedResponseDto<UserDto>>(result);
	}

	public async Task<UserDto> GetUserByEmail(string email)
	{
		var query = new GetUserByEmailQuery(email) ?? throw new NullReferenceException(Constants.EntityErrorNotLoaded);
		var result = await _mediator.Send(query);
		return _mapper.Map<UserDto>(result);
	}

	public async Task<UserDto> GetUserById(Guid id)
	{
		var query = new GetUserByIdQuery(id) ?? throw new NullReferenceException(Constants.EntityErrorNotLoaded);
		var result = await _mediator.Send(query);
		return _mapper.Map<UserDto>(result);
	}

	public async Task<UserDto> UpdateUser(UserUpdateDto dto)
	{
		var command = _mapper.Map<UserUpdateCommand>(dto);
		var result = await _mediator.Send(command);
		return _mapper.Map<UserDto>(result);
	}
}
