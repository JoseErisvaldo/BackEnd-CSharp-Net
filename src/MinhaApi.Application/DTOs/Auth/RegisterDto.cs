using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs.Auth;

public record RegisterDto(
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    string Name,
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    string Email,
    [Phone(ErrorMessage = "WhatsApp inválido")]
    string? Whatsapp,
    [Required(ErrorMessage = "Senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
    string Password
);