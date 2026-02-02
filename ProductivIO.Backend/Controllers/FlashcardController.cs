using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Flashcards;
using ProductivIO.Backend.Services.Interfaces;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers
{
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

        // GET: /api/Flashcard
        [HttpGet]
        public async Task<IActionResult> GetAllFlashcards()
        {
            var userId = GetUserId();
            var flashcards = await _flashcardService.GetAllFlashcardsAsync(userId);
            return Ok(flashcards);
        }

        // GET: /api/Flashcard/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlashcard(Guid id)
        {
            var userId = GetUserId();
            var flashcard = await _flashcardService.GetFlashcardAsync(id, userId);
            if (flashcard == null)
                return NotFound(new { message = "Flashcard deck not found." });
            return Ok(flashcard);
        }

        // POST: /api/Flashcard
        [HttpPost]
        public async Task<IActionResult> AddFlashcard([FromBody] CreateFlashcardDto flashcard)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _flashcardService.AddFlashcardAsync(flashcard, userId);
            if (created == null) return BadRequest(new { message = "Failed to create flashcard deck." });

            return CreatedAtAction(nameof(GetFlashcard), new { id = created.Id }, created);
        }

        // PUT: /api/Flashcard/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlashcard(Guid id, [FromBody] UpdateFlashcardDto flashcard)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _flashcardService.UpdateFlashcardAsync(id, flashcard, userId);
            if (updated == null)
                return NotFound(new { message = "Flashcard deck not found or you don't have permission." });

            return Ok(updated);
        }

        // DELETE: /api/Flashcard/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlashcard(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _flashcardService.DeleteFlashcardAsync(id, userId);
            if (!deleted)
                return NotFound(new { message = "Flashcard deck not found or you don't have permission." });

            return NoContent();
        }

        // Flashcard Questions
        // POST: /api/Flashcard/{flashcardId}/questions
        [HttpPost("{flashcardId}/questions")]
        public async Task<IActionResult> AddQuestion(Guid flashcardId, [FromBody] CreateFlashcardQuestionDto question)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _flashcardService.AddQuestionAsync(flashcardId, question, userId);
            if (created == null)
                return BadRequest(new { message = "Failed to add question or flashcard deck not found." });
            return Ok(created);
        }

        // PUT: /api/Flashcard/questions/{id}
        [HttpPut("questions/{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] UpdateFlashcardQuestionDto question)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _flashcardService.UpdateQuestionAsync(id, question, userId);
            if (updated == null)
                return NotFound(new { message = "Question not found or you don't have permission." });
            return Ok(updated);
        }

        // DELETE: /api/Flashcard/questions/{questionId}
        [HttpDelete("questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
            var userId = GetUserId();
            var deleted = await _flashcardService.DeleteQuestionAsync(questionId, userId);
            if (!deleted)
                return NotFound(new { message = "Question not found or you don't have permission." });
            return NoContent();
        }

        // Flashcard Answers
        // POST: /api/Flashcard/questions/{questionId}/answers
        [HttpPost("questions/{questionId}/answers")]
        public async Task<IActionResult> AddAnswer(Guid questionId, [FromBody] CreateFlashcardAnswerDto answer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _flashcardService.AddAnswerAsync(questionId, answer, userId);
            if (created == null)
                return BadRequest(new { message = "Failed to add answer or question not found." });
            return Ok(created);
        }

        // PUT: /api/Flashcard/answers/{id}
        [HttpPut("answers/{id}")]
        public async Task<IActionResult> UpdateAnswer(Guid id, [FromBody] UpdateFlashcardAnswerDto answer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _flashcardService.UpdateAnswerAsync(id, answer, userId);
            if (updated == null)
                return NotFound(new { message = "Answer not found or you don't have permission." });
            return Ok(updated);
        }

        // DELETE: /api/Flashcard/answers/{answerId}
        [HttpDelete("answers/{answerId}")]
        public async Task<IActionResult> DeleteAnswer(Guid answerId)
        {
            var userId = GetUserId();
            var deleted = await _flashcardService.DeleteAnswerAsync(answerId, userId);
            if (!deleted)
                return NotFound(new { message = "Answer not found or you don't have permission." });
            return NoContent();
        }
    }
}
