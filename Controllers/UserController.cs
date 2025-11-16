using Microsoft.AspNetCore.Mvc;
using MinhaApi.Entities;
using MinhaApi.Services;

namespace MinhaApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDto dto)
    {
        var user = await _service.Create(dto);
        return Created("", user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UserDto dto)

    {
        var user = await _service.Update(id, dto);
        if (user == null) return NotFound();
        return Ok(user);
    }
}
