

using MinhaApi.Entities;

namespace MinhaApi.Repositories;

public interface IEstablishmentRepository
{
    Task<List<Establishments>> GetAllAsync();
    Task<Establishments?> GetByIdAsync(Guid id);
    Task<Establishments> AddAsync(Establishments establishments);
    Task<Establishments> UpdateAsync(Establishments establishments);
    Task<bool> DeleteAsync(Guid id);
}
