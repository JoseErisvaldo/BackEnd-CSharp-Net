namespace MinhaApi.DTOs.Establishments;

public record EstablishmentResponseDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt
);