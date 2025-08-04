using Domain.Entities;
using MediatR;

namespace Application.Mediator.Users.Commands;

public class UserCommand : IRequest<User>
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string HashedPassword { get; set; } = string.Empty;
	public string Salt { get; set; } = string.Empty;
}
