using Microsoft.AspNetCore.Mvc;
using ProductivIOBackend.Services.Interfaces;
using ProductivIOBackend.DTOs.Flashcards;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlashcardController : ControllerBase
    {
        private readonly IFlashcardService _flashcardService;

        public FlashcardController(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        // GET: /api/Flashcard/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllFlashcards([FromQuery] Guid userId)
        {
            var flashcards = await _flashcardService.GetAllFlashcardsAsync(userId);
            return Ok(flashcards);
        }

        // GET: /api/Flashcard/{id}/user/{userId}
        [HttpGet("{id}/user/{userId}")]
        public async Task<IActionResult> GetFlashcard(Guid id, [FromQuery] Guid userId)
        {
            var flashcard = await _flashcardService.GetFlashcardAsync(id, userId);
            if (flashcard == null)
                return NotFound();
            return Ok(flashcard);
        }

        // POST: /api/Flashcard
        [HttpPost]
        public async Task<IActionResult> AddFlashcard([FromBody] FlashcardsDto flashcard)
        {
            var created = await _flashcardService.AddFlashcardAsync(flashcard);
            if (created == null) return BadRequest("Failed to create flashcard.");

            return CreatedAtAction(nameof(GetFlashcard), new { id = created.Id, userId = created.UserId }, created);
        }

        // PUT: /api/Flashcard/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlashcard(Guid id, [FromBody] FlashcardsDto flashcard)
        {
            if (id != flashcard.Id)
                return BadRequest("ID mismatch.");

            var updated = await _flashcardService.UpdateFlashcardAsync(flashcard);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: /api/Flashcard/{id}/user/{userId}
        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> DeleteFlashcard(Guid id, [FromQuery] Guid userId)
        {
            var deleted = await _flashcardService.DeleteFlashcardAsync(id, userId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // Flashcard Questions
        // POST: /api/Flashcard/{flashcardId}/questions
        [HttpPost("{flashcardId}/questions")]
        public async Task<IActionResult> AddQuestion(Guid flashcardId, [FromBody] FlashcardQuestionDto question)
        {
            var created = await _flashcardService.AddQuestionAsync(flashcardId, question);
            if (created == null)
                return BadRequest("Failed to add question.");
            return Ok(created);
        }

        // PUT: /api/Flashcard/questions/{id}
        [HttpPut("questions/{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] FlashcardQuestionDto question)
        {
            if (id != question.Id)
                return BadRequest("Question ID mismatch.");

            var updated = await _flashcardService.UpdateQuestionAsync(question);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: /api/Flashcard/questions/{questionId}
        [HttpDelete("questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
            var deleted = await _flashcardService.DeleteQuestionAsync(questionId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        // Flashcard Answers
        // POST: /api/Flashcard/questions/{questionId}/answers
        [HttpPost("questions/{questionId}/answers")]
        public async Task<IActionResult> AddAnswer(Guid questionId, [FromBody] FlashcardAnswerDto answer)
        {
            var created = await _flashcardService.AddAnswerAsync(questionId, answer);
            if (created == null)
                return BadRequest("Failed to add answer.");
            return Ok(created);
        }

        // PUT: /api/Flashcard/answers/{id}
        [HttpPut("answers/{id}")]
        public async Task<IActionResult> UpdateAnswer(Guid id, [FromBody] FlashcardAnswerDto answer)
        {
            if (id != answer.Id)
                return BadRequest("Answer ID mismatch.");

            var updated = await _flashcardService.UpdateAnswerAsync(answer);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: /api/Flashcard/answers/{answerId}
        [HttpDelete("answers/{answerId}")]
        public async Task<IActionResult> DeleteAnswer(Guid answerId)
        {
            var deleted = await _flashcardService.DeleteAnswerAsync(answerId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
        
    }
}
