using Application.Mediator.Users.Queries;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Pagination;
using MediatR;

namespace Application.Mediator.Users.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResponse<User>>
{
	private readonly IUserRepository _userRepository;

	public GetAllUsersQueryHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	public async Task<PagedResponse<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
	{
		return await _userRepository.GetAllUsersAsync(request.PageNumber, request.PageSize);
	}
}
