using Domain.Entities;
using Domain.Interfaces;
using Domain.Pagination;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
	readonly ApplicationDbContext _context;

	public UserRepository(ApplicationDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<User> CreateUserAsync(User entity)
	{
		_context.Add(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task<PagedResponse<User>> GetAllUsersAsync(int pageNumber, int pageSize)
	{
		var totalRecords = await _context.User.AsNoTracking().CountAsync();

		var users = await _context.User.AsNoTracking()
			.OrderBy(p => p.Name)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		var pagedResponse = new PagedResponse<User>(users, pageNumber, pageSize, totalRecords);

		return pagedResponse;
	}

	public async Task<User> GetUserByEmailAsync(string email)
	{
		IQueryable<User> query = (
			from c in _context.User.AsNoTracking()
			where c.Email == email
			select c);

		return await query.OrderBy(p => p.Name).FirstOrDefaultAsync();
	}

	public async Task<User> GetUserByIdAsync(Guid id)
	{
		IQueryable<User> query = (
			from c in _context.User.AsNoTracking()
			where c.Id == id
			select c);

		return await query.OrderBy(p => p.Name).FirstOrDefaultAsync();
	}

	public async Task<User> UpdateUserAsync(User entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		_context.Update(entity);
		await _context.SaveChangesAsync();
		return entity;
	}
}
