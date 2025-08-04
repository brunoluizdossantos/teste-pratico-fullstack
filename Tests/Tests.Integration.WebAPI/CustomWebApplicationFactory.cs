using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Tests.Integration.WebAPI;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
	public Mock<IAuthService> AuthServiceMock { get; } = new();
	public Mock<IUserService> UserServiceMock { get; } = new();
	public Mock<IValidator<UserCreateDto>> UserCreateValidatorMock { get; } = new();
	public Mock<IValidator<UserLoginDto>> UserLoginValidatorMock { get; } = new();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			// Remove serviços originais
			services.RemoveAll(typeof(IAuthService));
			services.RemoveAll(typeof(IUserService));
			services.RemoveAll(typeof(IValidator<UserCreateDto>));
			services.RemoveAll(typeof(IValidator<UserLoginDto>));

			// Adiciona mocks
			services.AddSingleton(UserCreateValidatorMock.Object);
			services.AddSingleton(UserLoginValidatorMock.Object);
		});
	}
}