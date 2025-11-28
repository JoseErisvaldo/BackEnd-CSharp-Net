using MinhaApi.DTOs.Establishments;

namespace MinhaApi.Application.Interfaces;

public interface IEstablishmentService
{
    Task<IEnumerable<EstablishmentResponseDto>> GetAllAsync();
    Task<EstablishmentResponseDto?> GetByIdAsync(Guid id);
    Task<EstablishmentResponseDto> CreateAsync(CreateEstablishmentDto dto);
    Task<EstablishmentResponseDto?> UpdateAsync(Guid id, UpdateEstablishmentDto dto);
    Task<bool> DeleteAsync(Guid id);
}