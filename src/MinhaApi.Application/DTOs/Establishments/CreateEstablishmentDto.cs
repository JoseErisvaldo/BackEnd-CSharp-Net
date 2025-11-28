using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs.Establishments;

public record CreateEstablishmentDto(
    [property: Required] string Name,
    string? Description
);
