using System.ComponentModel.DataAnnotations;

namespace MinhaApi.DTOs.Establishments;

public record CreateEstablishmentDto(
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
    string Name,
    [StringLength(1000, ErrorMessage = "Descrição muito longa")]
    string? Description
);