using Microsoft.AspNetCore.Mvc;

using MinhaApi.Application.Interfaces;
using MinhaApi.DTOs.Auth;

namespace MinhaApi.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MinhaApi.DTOs.Users.UserResponseDto>> Register([FromBody] RegisterDto dto)
    {
        var user = await _authService.RegisterAsync(dto);
        var result = _authService.MapToResponse(user);
        return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<object>> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        return Ok(new { token, message = "Login realizado com sucesso" });
    }
}