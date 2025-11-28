using AutoMapper;
using MinhaApi.Domain.Entities;
using MinhaApi.DTOs.Products;

namespace MinhaApi.Infrastructure.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponseDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
