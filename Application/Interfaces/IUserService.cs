using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
	Task<UserResponseDto> CreateUser(UserCreateDto dto);
	Task<PagedResponseDto<UserDto>> GetAllUsers(PagedRequestDto dto);
	Task<UserDto> GetUserByEmail(string email);
	Task<UserDto> GetUserById(Guid id);
	Task<UserDto> UpdateUser(UserUpdateDto dto);
}