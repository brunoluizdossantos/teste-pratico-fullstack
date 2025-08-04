using Application.DTOs;
using Application.Interfaces;
using Domain.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;

public class AuthService : IAuthService
{
	private readonly IConfiguration _configuration;
	private readonly IUserService _userService;

	public AuthService(IConfiguration configuration, IUserService userService)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}

	public async Task<UserTokenDto> LoginUser(UserLoginDto dto)
	{
		var userEmailExists = await _userService.GetUserByEmail(dto.Email);

		if (userEmailExists == null)
			throw new ArgumentException(Constants.UserNotFound);

		this.CheckValidationPassword(dto.Password + userEmailExists.Salt, userEmailExists.HashedPassword);

		var token = this.GenerateJwtToken(userEmailExists.Email);

		return token;
	}

	private void CheckValidationPassword(string pepper, string hashedPassword)
	{
		var isValidPassword = BCrypt.Net.BCrypt.Verify(pepper, hashedPassword);
		
		if (!isValidPassword)
			throw new ArgumentException(Constants.UserInvalidPassword);
	}

	private UserTokenDto GenerateJwtToken(string email)
	{
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

		var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

		var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["Jwt:ExpiryMinutes"]));

		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: expiration,
			signingCredentials: credentials
		);

		return new UserTokenDto()
		{
			Token = new JwtSecurityTokenHandler().WriteToken(token),
			Expiration = expiration
		};
	}
}
