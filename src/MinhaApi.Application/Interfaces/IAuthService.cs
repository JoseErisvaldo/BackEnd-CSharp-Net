using MinhaApi.Domain.Entities;
using MinhaApi.DTOs.Auth;
using MinhaApi.DTOs.Users;

namespace MinhaApi.Application.Interfaces;

public interface IAuthService
{
    Task<User> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
    UserResponseDto MapToResponse(User user);
}