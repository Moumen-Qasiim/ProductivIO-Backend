using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Contracts.Requests.Quiz;
using ProductivIO.Contracts.Responses.Quiz;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Guid.Empty;
        return Guid.Parse(userIdClaim.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizResponse>>> GetAll()
    {
        var userId = GetUserId();
        return Ok(await _quizService.GetAllAsync(userId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuizResponse>> Get(Guid id)
    {
        var userId = GetUserId();
        var quiz = await _quizService.GetByIdAsync(id, userId);
        if (quiz == null) return NotFound();
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<ActionResult<QuizResponse>> Create([FromBody] CreateQuizRequest request)
    {
        var userId = GetUserId();
        var created = await _quizService.CreateAsync(request, userId);
        return CreatedAtAction(nameof(Get), new { id = created?.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuizRequest request)
    {
        var userId = GetUserId();
        var success = await _quizService.UpdateAsync(id, request, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var success = await _quizService.DeleteAsync(id, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpPost("{id}/questions")]
    public async Task<ActionResult<QuizQuestionResponse>> AddQuestion(Guid id, [FromBody] CreateQuizQuestionRequest request)
    {
        var userId = GetUserId();
        var created = await _quizService.AddQuestionAsync(id, request, userId);
        if (created == null) return NotFound();
        return Ok(created);
    }

    [HttpPost("submit")]
    public async Task<ActionResult<QuizResultResponse>> Submit([FromBody] SubmitQuizResultRequest request)
    {
        var userId = GetUserId();
        var result = await _quizService.SubmitResultAsync(request, userId);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
