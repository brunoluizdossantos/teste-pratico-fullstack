using Domain.Entities;
using Domain.Pagination;
using MediatR;

namespace Application.Mediator.Users.Queries;

public class GetAllUsersQuery : IRequest<PagedResponse<User>>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }

	public GetAllUsersQuery(int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}
