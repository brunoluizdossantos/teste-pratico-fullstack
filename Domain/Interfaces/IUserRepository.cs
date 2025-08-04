using Domain.Entities;
using Domain.Pagination;

namespace Domain.Interfaces;

public interface IUserRepository
{
	Task<User> CreateUserAsync(User entity);
	Task<PagedResponse<User>> GetAllUsersAsync(int pageNumber, int pageSize);
	Task<User> GetUserByEmailAsync(string email);
	Task<User> GetUserByIdAsync(Guid id);
	Task<User> UpdateUserAsync(User entity);
}
