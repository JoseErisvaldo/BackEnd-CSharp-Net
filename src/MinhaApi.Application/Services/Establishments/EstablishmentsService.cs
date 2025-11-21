
using AutoMapper;
using MinhaApi.DTOs;
using MinhaApi.Entities;
using MinhaApi.Repositories;

namespace MinhaApi.Services;

public class EstablishmentsService : IEstablishmentsService
{
    private readonly IEstablishmentRepository _repo;
    private readonly IMapper _mapper;

    public EstablishmentsService(IEstablishmentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public Task<List<Establishments>> GetAll() => _repo.GetAllAsync();
    public Task<Establishments?> GetById(Guid id) => _repo.GetByIdAsync(id);

    public async Task<Establishments> Create(CreateEstablishmentDto dto)
    {
        var establishment = _mapper.Map<Establishments>(dto);
        return await _repo.AddAsync(establishment);
    }

    public async Task<Establishments?> Update(Guid id, UpdateEstablishmentDto dtos)
    {
        var establishment = await _repo.GetByIdAsync(id);
        if (establishment == null) return null;

        _mapper.Map(dtos, establishment);
        return await _repo.UpdateAsync(establishment);
    }

    public Task<bool> Delete(Guid id) => _repo.DeleteAsync(id);
}