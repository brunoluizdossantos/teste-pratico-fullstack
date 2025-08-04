namespace Application.DTOs;

public class GeneratePasswordDto
{
	public string Password { get; set; } = string.Empty;
	public string Hash { get; set; } = string.Empty;
	public string Salt { get; set; } = string.Empty;
}