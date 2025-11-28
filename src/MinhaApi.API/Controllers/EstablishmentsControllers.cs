using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MinhaApi.Application.Interfaces;
using MinhaApi.DTOs.Establishments;

namespace MinhaApi.API.Controllers;

[Authorize]
[ApiController]
[Route("api/establishments")]
public class EstablishmentsController : ControllerBase
{
    private readonly IEstablishmentService _service;

    public EstablishmentsController(IEstablishmentService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EstablishmentResponseDto>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstablishmentResponseDto>> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstablishmentResponseDto>> Create([FromBody] CreateEstablishmentDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstablishmentResponseDto>> Update(Guid id, [FromBody] UpdateEstablishmentDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var removed = await _service.DeleteAsync(id);
        if (!removed) return NotFound();
        return NoContent();
    }
}