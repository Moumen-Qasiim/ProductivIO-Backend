using Microsoft.AspNetCore.Mvc;
using ProductivIOBackend.Models;
using ProductivIOBackend.Services.Interfaces;
using System.Threading.Tasks;
using ProductivIOBackend.DTOs.Notes;

namespace ProductivIOBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: api/Notes/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var notes = await _noteService.GetAll(userId);
            return Ok(notes);
        }

        // GET: api/Notes/{id}/user/{userId}
        [HttpGet("{id}/user/{userId}")]
        public async Task<IActionResult> Get(int id, int userId)
        {
            var note = await _noteService.Get(id, userId);
            if (note == null)
                return NotFound(new { message = "Note not found." });

            return Ok(note);
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteDto note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _noteService.Create(note);
            if (created == null)
                return BadRequest(new { message = "Could not create note." });

            return CreatedAtAction(nameof(Get), new { id = created.Id, userId = created.UserID }, created);
        }

        // PUT: api/Notes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NoteDto note)
        {
            if (id != note.Id)
                return BadRequest(new { message = "Note ID mismatch." });

            var updated = await _noteService.Update(id, note);
            if (!updated)
                return NotFound(new { message = "Note not found for update." });

            return Ok(new { message = "Note updated successfully." });
        }

        // DELETE: api/Notes/{id}/user/{userId}
        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> Delete(int id, int userId)
        {
            var deleted = await _noteService.Delete(id, userId);
            if (!deleted)
                return NotFound(new { message = "Note not found for deletion." });

            return NoContent();
        }
    }
}
