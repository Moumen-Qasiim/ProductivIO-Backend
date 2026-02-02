using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Notes;
using ProductivIO.Backend.Services.Interfaces;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers
{
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

        // GET: api/Notes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var notes = await _noteService.GetAll(userId);
            return Ok(notes);
        }

        // GET: api/Notes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = GetUserId();
            var note = await _noteService.Get(id, userId);
            if (note == null)
                return NotFound(new { message = "Note not found." });

            return Ok(note);
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNoteDto note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _noteService.Create(note, userId);
            if (created == null)
                return BadRequest(new { message = "Could not create note." });

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/Notes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteDto note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var success = await _noteService.Update(id, note, userId);
            if (!success)
                return NotFound(new { message = "Note not found or you don't have permission to update it." });

            return Ok(new { message = "Note updated successfully." });
        }

        // DELETE: api/Notes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _noteService.Delete(id, userId);
            if (!deleted)
                return NotFound(new { message = "Note not found or you don't have permission to delete it." });

            return NoContent();
        }
    }
}
