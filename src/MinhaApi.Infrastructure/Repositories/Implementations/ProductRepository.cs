using MinhaApi.Entities;
using MinhaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MinhaApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        Console.WriteLine($"Buscando produto com ID: {id}");
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
