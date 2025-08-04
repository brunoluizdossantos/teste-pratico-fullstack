using Application.Mediator.Users.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Mediator.Users.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
	private readonly IUserRepository _userRepository;

	public GetUserByIdQueryHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		return await _userRepository.GetUserByIdAsync(request.Id);
	}
}
