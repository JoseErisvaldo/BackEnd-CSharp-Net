

using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Entities;

namespace MinhaApi.Repositories;

public class EstablishmentsRepository : IEstablishmentRepository
{
    private readonly AppDbContext _context;
    public EstablishmentsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Establishments>> GetAllAsync()
    {
        return await _context.Establishments.ToListAsync();
    }

    public async Task<Establishments?> GetByIdAsync(Guid id)
    {
        return await _context.Establishments.FirstOrDefaultAsync(byId => byId.Id == id);
    }

    public async Task<Establishments> AddAsync(Establishments establishments)
    {
        _context.Establishments.Add(establishments);
        await _context.SaveChangesAsync();
        return establishments;
    }

    public async Task<Establishments> UpdateAsync(Establishments establishments)
    {
        _context.Establishments.Update(establishments);
        await _context.SaveChangesAsync();
        return establishments;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var establishments = await GetByIdAsync(id);
        if (establishments == null) return false;

        _context.Establishments.Remove(establishments);
        await _context.SaveChangesAsync();
        return true;
    }
}