using AutoMapper;

using MinhaApi.Application.Interfaces;
using MinhaApi.Domain.Entities;
using MinhaApi.DTOs.Users;
using MinhaApi.Infrastructure.Repositories.Interfaces;

namespace MinhaApi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetByIdAsync(Guid id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user is null) return null;
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user is null) return null;
        _mapper.Map(dto, user);
        await _repo.UpdateAsync(user);
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user is null) return false;
        await _repo.DeleteAsync(user);
        return true;
    }
}