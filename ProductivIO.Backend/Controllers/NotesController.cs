using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Contracts.Requests.Notes;
using ProductivIO.Contracts.Responses.Notes;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Guid.Empty;
        return Guid.Parse(userIdClaim.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteResponse>>> GetAll()
    {
        var userId = GetUserId();
        var notes = await _noteService.GetAllAsync(userId);
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteResponse>> Get(Guid id)
    {
        var userId = GetUserId();
        var note = await _noteService.GetByIdAsync(id, userId);
        if (note == null)
            return NotFound(new { message = "Note not found." });

        return Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<NoteResponse>> Create([FromBody] CreateNoteRequest request)
    {
        var userId = GetUserId();
        var created = await _noteService.CreateAsync(request, userId);
        if (created == null)
            return BadRequest(new { message = "Could not create note." });

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteRequest request)
    {
        var userId = GetUserId();
        var success = await _noteService.UpdateAsync(id, request, userId);
        if (!success)
            return NotFound(new { message = "Note not found." });

        return Ok(new { message = "Note updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var success = await _noteService.DeleteAsync(id, userId);
        if (!success)
            return NotFound(new { message = "Note not found." });

        return NoContent();
    }
}
