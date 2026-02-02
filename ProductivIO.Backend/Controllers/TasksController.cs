using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Tasks;
using ProductivIO.Backend.Services.Interfaces;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Guid.Empty;
            return Guid.Parse(userIdClaim.Value);
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var tasks = await _taskService.GetAll(userId);
            return Ok(tasks);
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = GetUserId();
            var task = await _taskService.Get(id, userId);
            if (task == null)
                return NotFound(new { message = "Task not found." });

            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _taskService.Create(task, userId);
            if (created == null)
                return BadRequest(new { message = "Could not create task." });

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/Tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var success = await _taskService.Update(id, task, userId);
            if (!success)
                return NotFound(new { message = "Task not found or you don't have permission to update it." });

            return Ok(new { message = "Task updated successfully." });
        }

        // DELETE: api/Tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _taskService.Delete(id, userId);
            if (!deleted)
                return NotFound(new { message = "Task not found or you don't have permission to delete it." });

            return NoContent();
        }
    }
}
