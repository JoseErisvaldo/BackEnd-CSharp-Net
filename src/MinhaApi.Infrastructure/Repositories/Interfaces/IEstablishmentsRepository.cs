using MinhaApi.Domain.Entities;

namespace MinhaApi.Infrastructure.Repositories.Interfaces;

public interface IEstablishmentRepository
{
    Task<List<Establishment>> GetAllAsync();
    Task<Establishment?> GetByIdAsync(Guid id);
    Task AddAsync(Establishment establishment);
    Task UpdateAsync(Establishment establishment);
    Task DeleteAsync(Establishment establishment);
}
