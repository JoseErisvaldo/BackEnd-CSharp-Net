using AutoMapper;
using MinhaApi.Application.Interfaces;
using MinhaApi.DTOs.Products;
using MinhaApi.Domain.Entities;
using MinhaApi.Infrastructure.Repositories.Interfaces;

namespace MinhaApi.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAll()
    {
        var products = await _repository.GetAll();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto?> GetById(int id)
    {
        var product = await _repository.GetById(id);
        if (product == null) return null;
        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> Create(CreateProductDto dto)
    {
        var entity = _mapper.Map<Product>(dto);
        await _repository.Add(entity);
        return _mapper.Map<ProductResponseDto>(entity);
    }

    public async Task<ProductResponseDto?> Update(int id, UpdateProductDto dto)
    {
        var product = await _repository.GetById(id);
        if (product == null) return null;
        _mapper.Map(dto, product);
        await _repository.Update(product);
        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _repository.GetById(id);
        if (product == null) return false;
        await _repository.Delete(product);
        return true;
    }
}
