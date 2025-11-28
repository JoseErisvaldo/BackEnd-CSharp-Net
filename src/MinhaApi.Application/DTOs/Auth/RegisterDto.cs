using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs.Auth;

public record RegisterDto(
    [Required] string Name,
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password
);
