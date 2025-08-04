using Application.DTOs;
using Application.Interfaces;
using Domain.Utils;
using System.Security.Cryptography;

namespace Application.Services;

public class UtilService : IUtilService
{
	public GeneratePasswordDto GeneratePassword()
	{
		var randomPassword = RandomNumberGenerator.GetString(Constants.TestAllowedChars, Constants.DefaultPasswordLength);
		var salt = Environment.TickCount.ToString();
		var pepper = randomPassword + salt;
		var hash = BCrypt.Net.BCrypt.HashPassword(pepper);

		return new GeneratePasswordDto()
		{
			Password = randomPassword,
			Hash = hash,
			Salt = salt
		};
	}
}
