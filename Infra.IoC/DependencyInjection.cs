using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Application.Utils.Filters;
using Domain.Interfaces;
using Domain.Utils;
using FluentValidation;
using Infra.Data.Context;
using Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Infra.IoC;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var host = configuration["DBHOST"] ?? "localhost";
		var port = configuration["DBPORT"] ?? "3306";
		var password = configuration["MYSQL_PASSWORD"] ?? configuration.GetConnectionString("MYSQL_PASSWORD");
		var userid = configuration["MYSQL_USER"] ?? configuration.GetConnectionString("MYSQL_USER");
		var database = configuration["MYSQL_DATABASE"] ?? configuration.GetConnectionString("MYSQL_DATABASE");

		string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={database}";

		services.AddDbContextPool<ApplicationDbContext>(options =>
			options.UseMySql(mySqlConnStr, ServerVersion.AutoDetect(mySqlConnStr),
			b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

		services.AddScoped<IUserRepository, UserRepository>();

		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IUtilService, UtilService>();

		services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();
		services.AddValidatorsFromAssemblyContaining<UserUpdateDtoValidator>();
		services.AddValidatorsFromAssemblyContaining<PagedRequestDtoValidator>();
		services.AddValidatorsFromAssemblyContaining<UserLoginDtoValidator>();

		services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

		services.AddMediatR(AppDomain.CurrentDomain.Load("Application"));

		return services;
	}

	public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSwaggerGen(o =>
		{
			o.EnableAnnotations();

			o.SchemaFilter<SwaggerSchemaExampleFilter>();

			o.SwaggerDoc("v1",
				new OpenApiInfo
				{
					Title = Constants.ProjectTitle,
					Version = "v1.0",
					Description = Constants.ProjectDescription,
					Contact = new OpenApiContact() { Name = Constants.ProjectContactName, Email = Constants.ProjectContactEmail },
					License = new OpenApiLicense() { Name = Constants.ProjectLicenseName, Url = new Uri(Constants.ProjectLicenseUrl) }
				});

			o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = Constants.JwtAuthorizationDescription
			});

			var securityRequirement = new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Id = "Bearer",
								Type = ReferenceType.SecurityScheme
							},
							In = ParameterLocation.Header,
							Name = "Bearer",
							Scheme = "Bearer"
						},
						new string[] { }
					}
				};

			o.AddSecurityRequirement(securityRequirement);
		});

		services.AddAuthorization();
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					ValidAudience = configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])
					),
					ClockSkew = TimeSpan.Zero
				};
			});

		return services;
	}
}