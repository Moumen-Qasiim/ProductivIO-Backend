using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Contracts.Requests.Tasks;
using ProductivIO.Contracts.Responses.Tasks;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAll()
    {
        var userId = GetUserId();
        var tasks = await _taskService.GetAllAsync(userId);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> Get(Guid id)
    {
        var userId = GetUserId();
        var task = await _taskService.GetByIdAsync(id, userId);
        if (task == null)
            return NotFound(new { message = "Task not found." });

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create([FromBody] CreateTaskRequest request)
    {
        var userId = GetUserId();
        var created = await _taskService.CreateAsync(request, userId);
        if (created == null)
            return BadRequest(new { message = "Could not create task." });

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
    {
        var userId = GetUserId();
        var success = await _taskService.UpdateAsync(id, request, userId);
        if (!success)
            return NotFound(new { message = "Task not found." });

        return Ok(new { message = "Task updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var success = await _taskService.DeleteAsync(id, userId);
        if (!success)
            return NotFound(new { message = "Task not found." });

        return NoContent();
    }
}
