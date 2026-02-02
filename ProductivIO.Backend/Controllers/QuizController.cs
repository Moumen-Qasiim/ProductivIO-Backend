using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Quiz;
using ProductivIO.Backend.Services.Interfaces;
using System.Security.Claims;

namespace ProductivIO.Backend.Controllers
{
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

        // --- Quizzes ---
        // GET: /api/Quiz
        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var userId = GetUserId();
            var quizzes = await _quizService.GetAllQuizzes(userId);
            return Ok(quizzes);
        }

        // GET: /api/Quiz/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuiz(Guid id)
        {
            var userId = GetUserId();
            var quiz = await _quizService.GetQuiz(id, userId);
            if (quiz == null) return NotFound(new { message = "Quiz not found." });
            return Ok(quiz);
        }

        // POST: /api/Quiz
        [HttpPost]
        public async Task<IActionResult> AddQuiz([FromBody] CreateQuizDto quiz)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _quizService.AddQuiz(quiz, userId);
            if (created == null) return BadRequest(new { message = "Failed to create quiz." });

            return CreatedAtAction(nameof(GetQuiz), new { id = created.Id }, created);
        }

        // PUT: /api/Quiz/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(Guid id, [FromBody] UpdateQuizDto quiz)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _quizService.UpdateQuiz(id, quiz, userId);
            if (!updated) return NotFound(new { message = "Quiz not found or you don't have permission." });

            return NoContent();
        }

        // DELETE: /api/Quiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _quizService.DeleteQuiz(id, userId);
            if (!deleted) return NotFound(new { message = "Quiz not found or you don't have permission." });

            return NoContent();
        }

        // POST: /api/Quiz/{quizId}/questions
        [HttpPost("{quizId}/questions")]
        public async Task<IActionResult> AddQuestion(Guid quizId, [FromBody] CreateQuizQuestionDto question)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _quizService.AddQuestion(quizId, question, userId);
            if (created == null) return BadRequest(new { message = "Failed to add question or quiz not found." });

            return Ok(created);
        }

        // PUT: /api/Quiz/questions/{id}
        [HttpPut("questions/{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] UpdateQuizQuestionDto question)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _quizService.UpdateQuestion(id, question, userId);
            if (!updated) return NotFound(new { message = "Question not found or you don't have permission." });

            return NoContent();
        }

        // DELETE: /api/Quiz/questions/{questionId}
        [HttpDelete("questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
            var userId = GetUserId();
            var deleted = await _quizService.DeleteQuestion(questionId, userId);
            if (!deleted) return NotFound(new { message = "Question not found or you don't have permission." });

            return NoContent();
        }

        // --- Answers ---
        // POST: /api/Quiz/questions/{questionId}/answers
        [HttpPost("questions/{questionId}/answers")]
        public async Task<IActionResult> AddAnswer(Guid questionId, [FromBody] CreateQuizAnswerDto answer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            var created = await _quizService.AddAnswer(questionId, answer, userId);
            if (created == null) return BadRequest(new { message = "Failed to add answer or question not found." });

            return Ok(created);
        }

        // PUT: /api/Quiz/answers/{id}
        [HttpPut("answers/{id}")]
        public async Task<IActionResult> UpdateAnswer(Guid id, [FromBody] UpdateQuizAnswerDto answer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _quizService.UpdateAnswer(id, answer, userId);
            if (!updated) return NotFound(new { message = "Answer not found or you don't have permission." });

            return NoContent();
        }

        // DELETE: /api/Quiz/answers/{answerId}
        [HttpDelete("answers/{answerId}")]
        public async Task<IActionResult> DeleteAnswer(Guid answerId)
        {
            var userId = GetUserId();
            var deleted = await _quizService.DeleteAnswer(answerId, userId);
            if (!deleted) return NotFound(new { message = "Answer not found or you don't have permission." });

            return NoContent();
        }
    }
}
