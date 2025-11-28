namespace MinhaApi.DTOs.Users;

public record UpdateUserDto(
    string Name,
    string Email,
    string? Whatsapp
);
