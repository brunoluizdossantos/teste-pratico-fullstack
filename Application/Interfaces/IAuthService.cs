using Application.DTOs;

namespace Application.Interfaces;

public interface IAuthService
{
	Task<UserTokenDto> LoginUser(UserLoginDto dto);
}