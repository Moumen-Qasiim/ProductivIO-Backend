using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Pomodoro;
using ProductivIO.Backend.Services.Interfaces;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers
{
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

        // GET: api/Pomodoro
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var pomodoros = await _pomodoroService.GetAll(userId);
            return Ok(pomodoros);
        }

        // GET: api/Pomodoro/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = GetUserId();
            var pomodoro = await _pomodoroService.Get(id, userId);
            if (pomodoro == null)
                return NotFound(new { message = "Pomodoro session not found." });

            return Ok(pomodoro);
        }

        // POST: api/Pomodoro
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePomodoroDto pomodoro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _pomodoroService.Create(pomodoro, userId);
            if (created == null)
                return BadRequest(new { message = "Could not create Pomodoro session." });

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/Pomodoro/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePomodoroDto pomodoro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _pomodoroService.Update(id, pomodoro, userId);
            if (!updated)
                return NotFound(new { message = "Pomodoro session not found for update or you don't have permission." });

            return Ok(new { message = "Pomodoro updated successfully." });
        }

        // DELETE: api/Pomodoro/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _pomodoroService.Delete(id, userId);
            if (!deleted)
                return NotFound(new { message = "Pomodoro session not found for deletion or you don't have permission." });

            return NoContent();
        }

        // GET: api/Pomodoro/statistics/completed
        [HttpGet("statistics/completed")]
        public async Task<IActionResult> GetCompletedSession()
        {
            var userId = GetUserId();
            var count = await _pomodoroService.GetCompletedSession(userId);
            return Ok(count);
        }

        // GET: api/Pomodoro/statistics/duration
        [HttpGet("statistics/duration")]
        public async Task<IActionResult> GetTotalDuration()
        {
            var userId = GetUserId();
            var duration = await _pomodoroService.GetTotalDuration(userId);
            return Ok(duration);
        }
    }
}
