using MinhaApi.DTOs;
using MinhaApi.Entities;

namespace MinhaApi.Services;

public interface IEstablishmentsService
{
    Task<List<Establishments>> GetAll();
    Task<Establishments?> GetById(Guid id);
    Task<Establishments> Create(CreateEstablishmentDto dto);
    Task<Establishments?> Update(Guid id, UpdateEstablishmentDto dto);
    Task<bool> Delete(Guid id);
}

