namespace MinhaApi.Entities;

public class UserDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Whatsapp { get; set; }
    public string PasswordHash { get; set; } = string.Empty;

}
