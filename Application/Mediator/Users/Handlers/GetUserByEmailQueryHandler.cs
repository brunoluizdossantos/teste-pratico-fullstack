using Application.Mediator.Users.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Mediator.Users.Handlers;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
{
	private readonly IUserRepository _userRepository;

	public GetUserByEmailQueryHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
	{
		return await _userRepository.GetUserByEmailAsync(request.Email);
	}
}
