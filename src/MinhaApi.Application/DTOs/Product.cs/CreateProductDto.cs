using System.ComponentModel.DataAnnotations;


namespace MinhaApi.DTOs.Products;


public record CreateProductDto(
    [property: Required(ErrorMessage = "O nome é obrigatório"), StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")] string Name,
    [property: StringLength(500, ErrorMessage = "Descrição muito longa")] string? Description,
    [property: Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")] decimal Price
);