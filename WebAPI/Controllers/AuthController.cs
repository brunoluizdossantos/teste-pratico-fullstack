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
public class AuthController : Controller
{
	private readonly IValidator<UserLoginDto> _loginDtoValidator;
	private readonly IAuthService _authService;

	public AuthController(IValidator<UserLoginDto> loginDtoValidator, IAuthService authService)
	{
		_loginDtoValidator = loginDtoValidator ?? throw new ArgumentNullException(nameof(loginDtoValidator));
		_authService = authService ?? throw new ArgumentNullException(nameof(authService));
	}

	[HttpPost("login")]
	[SwaggerOperation(Summary = Constants.AuthTokenSchemaSummary, Description = Constants.AuthTokenSchemaDescription)]
	public async Task<ActionResult<UserTokenDto>> LoginUser([FromBody] UserLoginDto userLoginDto)
	{
		try
		{
			var validateData = await _loginDtoValidator.ValidateAsync(userLoginDto);

			if (!validateData.IsValid)
				return BadRequest(validateData.GetErrors());

			var token = await _authService.LoginUser(userLoginDto);

			return Ok(token);
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}
}
