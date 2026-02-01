using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Tasks;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: /api/Tasks/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            var tasks = await _taskService.GetAll(userId);
            return Ok(tasks);
        }

        // GET: /api/Tasks/{id}/user/{userId}
        [HttpGet("{id}/user/{userId}")]
        public async Task<IActionResult> Get(Guid id, Guid userId)
        {
            var task = await _taskService.Get(id, userId);
            if (task == null) return NotFound(new { message = "Task not found." });
            return Ok(task);
        }

        // POST: /api/Tasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskDto task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _taskService.Create(task);
            if (created == null) return BadRequest(new { message = "Failed to create task." });
            
            return CreatedAtAction(nameof(Get), new { id = created.Id, userId = created.UserId }, created);
        }

        // PUT: /api/Tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskDto task)
        {
            if (id != task.Id)
                return BadRequest(new { message = "Task ID mismatch." });

            var updated = await _taskService.Update(id, task);
            if (!updated) return NotFound(new { message = "Task not found or could not be updated." });

            return NoContent(); 
        }

        // DELETE: /api/Tasks/{id}/user/{userId}
        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            var deleted = await _taskService.Delete(id, userId);
            if (!deleted) return NotFound(new { message = "Task not found or could not be deleted." });

            return NoContent();
        }
    }
}
