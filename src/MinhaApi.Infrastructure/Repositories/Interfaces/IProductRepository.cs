using MinhaApi.Domain.Entities;

namespace MinhaApi.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(Product product);
}
