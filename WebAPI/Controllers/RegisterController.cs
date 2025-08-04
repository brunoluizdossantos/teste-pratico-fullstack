using Application.DTOs;
using Application.Interfaces;
using Application.Utils.Validations;
using Domain.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class RegisterController : Controller
{
	private readonly IValidator<UserCreateDto> _createDtoValidator;
	private readonly IUserService _userService;

	public RegisterController(IValidator<UserCreateDto> createDtoValidator, IUserService userService)
	{
		_createDtoValidator = createDtoValidator ?? throw new ArgumentNullException(nameof(createDtoValidator));
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}
	
	[HttpPost("user")]
	[SwaggerOperation(Summary = Constants.UserCreateSummary, Description = Constants.UserCreateDescription)]
	public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserCreateDto userDto)
	{
		try
		{
			var validateData = await _createDtoValidator.ValidateAsync(userDto);

			if (!validateData.IsValid)
				return BadRequest(validateData.GetErrors());

			var newUser = await _userService.CreateUser(userDto);

			return Ok(newUser);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}
}
