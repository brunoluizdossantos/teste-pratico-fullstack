using Domain.Entities;
using MediatR;

namespace Application.Mediator.Users.Queries;

public class GetUserByIdQuery : IRequest<User>
{
	public Guid Id { get; set; }

	public GetUserByIdQuery(Guid id)
	{
		Id = id;
	}
}