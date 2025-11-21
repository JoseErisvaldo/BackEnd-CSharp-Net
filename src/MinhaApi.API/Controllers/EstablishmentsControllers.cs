

using Microsoft.AspNetCore.Mvc;
using MinhaApi.DTOs;
using MinhaApi.Services;

namespace MinhaApi.Controllers;

[ApiController]
[Route("api/establishments")]
public class EstablishmentsController : ControllerBase
{
    private readonly IEstablishmentsService _service;

    public EstablishmentsController(IEstablishmentsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var establishments = await _service.GetById(id);
        if (establishments == null) return NotFound();
        return Ok(establishments);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEstablishmentDto dto)
    {
        var establishments = await _service.Create(dto);
        return Created("", establishments);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateEstablishmentDto dto)
    {
        var establishments = await _service.Update(id, dto);
        if (establishments == null) return NotFound();
        return Ok(establishments);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.Delete(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}