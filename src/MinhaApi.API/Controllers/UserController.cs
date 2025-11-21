using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Entities;
using MinhaApi.Services;

namespace MinhaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> GetAll()
    {
        var users = await _service.GetAllUsersAsync();
        var usersDto = _mapper.Map<List<UserResponseDto>>(users);
        return Ok(usersDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(Guid id)
    {
        var user = await _service.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        var userDto = _mapper.Map<UserResponseDto>(user);
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDto>> Update(Guid id, UserDto dto)
    {
        var user = await _service.Update(id, dto);
        if (user == null) return NotFound();

        var userDto = _mapper.Map<UserResponseDto>(user);
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.Delete(id);
        if (!ok) return NotFound();

        return NoContent();
    }
}
