using Application.DTOs;
using Application.Interfaces;
using Application.Utils.Validations;
using Domain.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class UsersController : Controller
{
	private readonly IValidator<UserUpdateDto> _updateDtoValidator;
	private readonly IValidator<PagedRequestDto> _pagedRequestDtoValidator;
	private readonly IUserService _userService;

	public UsersController(IValidator<UserUpdateDto> updateDtoValidator, IValidator<PagedRequestDto> pagedRequestDtoValidator, IUserService userService)
	{
		_updateDtoValidator = updateDtoValidator ?? throw new ArgumentNullException(nameof(updateDtoValidator));
		_pagedRequestDtoValidator = pagedRequestDtoValidator ?? throw new ArgumentNullException(nameof(pagedRequestDtoValidator));
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}

	[HttpGet]
	[SwaggerOperation(Summary = Constants.UserListSummary, Description = Constants.UserListDescription)]
	public async Task<ActionResult<PagedResponseDto<UserDto>>> GetAllUsers([FromQuery] PagedRequestDto dto)
	{
		try
		{
			var validateData = await _pagedRequestDtoValidator.ValidateAsync(dto);

			if (!validateData.IsValid)
				return BadRequest(validateData.GetErrors());

			var users = await _userService.GetAllUsers(dto);

			return Ok(users);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}

	[HttpGet("{id:Guid}")]
	[SwaggerOperation(Summary = Constants.UserItemSummary, Description = Constants.UserItemDescription)]
	public async Task<ActionResult<UserDto>> GetUser(Guid id)
	{
		try
		{
			var user = await _userService.GetUserById(id);

			if (user == null)
				return NotFound(Constants.UserNotFound);

			return Ok(user);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}

	[HttpPut("{id:Guid}/update")]
	[SwaggerOperation(Summary = Constants.UserUpdateSummary, Description = Constants.UserUpdateDescription)]
	public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UserUpdateDto userDto)
	{
		try
		{
			userDto.Id = id;

			var validateData = await _updateDtoValidator.ValidateAsync(userDto);

			if (!validateData.IsValid)
				return BadRequest(validateData.GetErrors());

			var newUser = await _userService.UpdateUser(userDto);

			return Ok(newUser);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}
}
