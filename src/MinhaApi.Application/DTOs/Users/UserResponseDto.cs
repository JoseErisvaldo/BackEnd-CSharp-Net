public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Whatsapp { get; set; }
    public string Role { get; set; } = "client";
    public DateTime CreatedAt { get; set; }
}
