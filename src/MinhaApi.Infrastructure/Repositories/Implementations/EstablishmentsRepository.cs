using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Domain.Entities;
using MinhaApi.Infrastructure.Repositories.Interfaces;

namespace MinhaApi.Infrastructure.Repositories.Implementations;

public class EstablishmentRepository : IEstablishmentRepository
{
    private readonly AppDbContext _context;

    public EstablishmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Establishment>> GetAllAsync()
        => await _context.Establishments.AsNoTracking().ToListAsync();

    public async Task<Establishment?> GetByIdAsync(Guid id)
        => await _context.Establishments.FindAsync(id);

    public async Task AddAsync(Establishment establishment)
    {
        await _context.Establishments.AddAsync(establishment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Establishment establishment)
    {
        _context.Establishments.Update(establishment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Establishment establishment)
    {
        _context.Establishments.Remove(establishment);
        await _context.SaveChangesAsync();
    }
}
