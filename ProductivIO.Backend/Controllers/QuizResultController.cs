using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Contracts.Responses.Quiz;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class QuizResultController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizResultController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Guid.Empty;
        return Guid.Parse(userIdClaim.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuizResultResponse>> Get(Guid id)
    {
        // This would normally be handled by a dedicated service or within QuizService
        // For now, I'll redirect to a logic that can fetch a result
        return NotFound(new { message = "Quiz result retrieval not fully implemented in this controller yet." });
    }
}
