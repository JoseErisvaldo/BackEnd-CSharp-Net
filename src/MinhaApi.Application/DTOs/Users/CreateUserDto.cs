namespace MinhaApi.DTOs.Users;

public record CreateUserDto(
    string Name,
    string Email,
    string? Whatsapp,
    string Password
);
