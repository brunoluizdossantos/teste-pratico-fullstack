using Application.Mediator.Users.Commands;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Utils;
using MediatR;

namespace Application.Mediator.Users.Handlers;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, User>
{
	private readonly IUserRepository _userRepository;

	public UserUpdateCommandHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	public async Task<User> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
	{
		var entity = await _userRepository.GetUserByIdAsync(request.Id);

		if (entity == null)
		{
			throw new ApplicationException(Constants.EntityErrorNotFound);
		}
		else
		{
			entity.Update(request.Name, request.Email);

			return await _userRepository.UpdateUserAsync(entity);
		}
	}
}
