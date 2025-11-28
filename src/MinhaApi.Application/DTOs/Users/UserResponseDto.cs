namespace MinhaApi.DTOs.Users;

public record UserResponseDto(
    Guid Id,
    string Name,
    string Email,
    string? Whatsapp,
    string Role,
    DateTime CreatedAt
);