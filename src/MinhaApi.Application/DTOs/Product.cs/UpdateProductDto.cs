namespace MinhaApi.DTOs.Products;

public record UpdateProductDto(string Name, string? Description, decimal Price) : CreateProductDto(Name, Description, Price);
