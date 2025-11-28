using MinhaApi.DTOs.Products;

namespace MinhaApi.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAll();
    Task<ProductResponseDto?> GetById(int id);
    Task<ProductResponseDto> Create(CreateProductDto dto);
    Task<ProductResponseDto?> Update(int id, UpdateProductDto dto);
    Task<bool> Delete(int id);
}
