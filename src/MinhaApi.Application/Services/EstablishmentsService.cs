using AutoMapper;
using MinhaApi.Application.Interfaces;
using MinhaApi.DTOs.Establishments;
using MinhaApi.Domain.Entities;
using MinhaApi.Infrastructure.Repositories.Interfaces;

namespace MinhaApi.Application.Services;

public class EstablishmentService : IEstablishmentService
{
    private readonly IEstablishmentRepository _repo;
    private readonly IMapper _mapper;

    public EstablishmentService(IEstablishmentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EstablishmentResponseDto>> GetAllAsync()
    {
        var entities = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<EstablishmentResponseDto>>(entities);
    }

    public async Task<EstablishmentResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return null;
        return _mapper.Map<EstablishmentResponseDto>(entity);
    }

    public async Task<EstablishmentResponseDto> CreateAsync(CreateEstablishmentDto dto)
    {
        var entity = _mapper.Map<Establishment>(dto);
        await _repo.AddAsync(entity);
        return _mapper.Map<EstablishmentResponseDto>(entity);
    }

    public async Task<EstablishmentResponseDto?> UpdateAsync(Guid id, UpdateEstablishmentDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return null;
        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
        return _mapper.Map<EstablishmentResponseDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity);
        return true;
    }
}
