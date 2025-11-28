using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Domain.Entities;
using MinhaApi.Infrastructure.Repositories.Interfaces;

namespace MinhaApi.Infrastructure.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
        => await _context.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetById(int id)
        => await _context.Products.FindAsync(id);

    public async Task Add(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
