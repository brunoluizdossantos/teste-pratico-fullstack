using Domain.Entities;
using MediatR;

namespace Application.Mediator.Users.Queries;

public class GetUserByEmailQuery : IRequest<User>
{
	public string Email { get; set; } = string.Empty;

	public GetUserByEmailQuery(string email)
	{
		Email = email;
	}
}