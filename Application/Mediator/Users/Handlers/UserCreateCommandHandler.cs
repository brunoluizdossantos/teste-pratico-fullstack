using Application.Mediator.Users.Commands;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Utils;
using MediatR;

namespace Application.Mediator.Users.Handlers;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, User>
{
	private readonly IUserRepository _userRepository;

	public UserCreateCommandHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	public async Task<User> Handle(UserCreateCommand request, CancellationToken cancellationToken)
	{
		var entity = new User(request.Name, request.Email, request.HashedPassword, request.Salt);

		if (entity == null)
			throw new ApplicationException(Constants.EntityErrorNotCreated);
		else
			return await _userRepository.CreateUserAsync(entity);
	}
}
