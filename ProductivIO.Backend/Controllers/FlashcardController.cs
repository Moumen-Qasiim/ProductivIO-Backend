using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Contracts.Requests.Flashcards;
using ProductivIO.Contracts.Responses.Flashcards;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FlashcardController : ControllerBase
{
    private readonly IFlashcardService _flashcardService;

    public FlashcardController(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Guid.Empty;
        return Guid.Parse(userIdClaim.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlashcardResponse>>> GetAll()
    {
        var userId = GetUserId();
        return Ok(await _flashcardService.GetAllAsync(userId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FlashcardResponse>> Get(Guid id)
    {
        var userId = GetUserId();
        var flashcard = await _flashcardService.GetByIdAsync(id, userId);
        if (flashcard == null) return NotFound();
        return Ok(flashcard);
    }

    [HttpPost]
    public async Task<ActionResult<FlashcardResponse>> Create([FromBody] CreateFlashcardRequest request)
    {
        var userId = GetUserId();
        var created = await _flashcardService.CreateAsync(request, userId);
        return CreatedAtAction(nameof(Get), new { id = created?.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFlashcardRequest request)
    {
        var userId = GetUserId();
        var success = await _flashcardService.UpdateAsync(id, request, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var success = await _flashcardService.DeleteAsync(id, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpPost("{id}/questions")]
    public async Task<ActionResult<FlashcardQuestionResponse>> AddQuestion(Guid id, [FromBody] CreateFlashcardQuestionRequest request)
    {
        var userId = GetUserId();
        var created = await _flashcardService.AddQuestionAsync(id, request, userId);
        if (created == null) return NotFound();
        return Ok(created);
    }

    [HttpPut("questions/{questionId}")]
    public async Task<IActionResult> UpdateQuestion(Guid questionId, [FromBody] UpdateFlashcardQuestionRequest request)
    {
        var userId = GetUserId();
        var success = await _flashcardService.UpdateQuestionAsync(questionId, request, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("questions/{questionId}")]
    public async Task<IActionResult> DeleteQuestion(Guid questionId)
    {
        var userId = GetUserId();
        var success = await _flashcardService.DeleteQuestionAsync(questionId, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpPost("questions/{questionId}/answers")]
    public async Task<ActionResult<FlashcardAnswerResponse>> AddAnswer(Guid questionId, [FromBody] CreateFlashcardAnswerRequest request)
    {
        var userId = GetUserId();
        var created = await _flashcardService.AddAnswerAsync(questionId, request, userId);
        if (created == null) return NotFound();
        return Ok(created);
    }

    [HttpPut("answers/{answerId}")]
    public async Task<IActionResult> UpdateAnswer(Guid answerId, [FromBody] UpdateFlashcardAnswerRequest request)
    {
        var userId = GetUserId();
        var success = await _flashcardService.UpdateAnswerAsync(answerId, request, userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("answers/{answerId}")]
    public async Task<IActionResult> DeleteAnswer(Guid answerId)
    {
        var userId = GetUserId();
        var success = await _flashcardService.DeleteAnswerAsync(answerId, userId);
        if (!success) return NotFound();
        return NoContent();
    }
}
