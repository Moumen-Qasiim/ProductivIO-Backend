using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Contracts.Requests.Pomodoro;
using ProductivIO.Contracts.Responses.Pomodoro;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PomodoroController : ControllerBase
{
    private readonly IPomodoroService _pomodoroService;

    public PomodoroController(IPomodoroService pomodoroService)
    {
        _pomodoroService = pomodoroService;
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Guid.Empty;
        return Guid.Parse(userIdClaim.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PomodoroResponse>>> GetAll()
    {
        var userId = GetUserId();
        return Ok(await _pomodoroService.GetAllAsync(userId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PomodoroResponse>> Get(Guid id)
    {
        var userId = GetUserId();
        var session = await _pomodoroService.GetByIdAsync(id, userId);
        if (session == null) return NotFound();
        return Ok(session);
    }

    [HttpPost]
    public async Task<ActionResult<PomodoroResponse>> Create([FromBody] CreatePomodoroRequest request)
    {
        var userId = GetUserId();
        var created = await _pomodoroService.CreateAsync(request, userId);
        return CreatedAtAction(nameof(Get), new { id = created?.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePomodoroRequest request)
    {
        var userId = GetUserId();
        var success = await _pomodoroService.UpdateAsync(id, request, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var success = await _pomodoroService.DeleteAsync(id, userId);
        if (!success) return NotFound();
        return NoContent();
    }
}
