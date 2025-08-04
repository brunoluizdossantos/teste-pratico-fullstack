namespace Application.DTOs;

public class UserTokenDto
{
	public string Token { get; set; } = string.Empty;
	public DateTime Expiration { get; set; }
}