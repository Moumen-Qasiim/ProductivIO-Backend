using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Quiz;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizResultController : ControllerBase
    {
        private readonly IQuizResultService _quizResultService;

        public QuizResultController(IQuizResultService quizResultService)
        {
            _quizResultService = quizResultService;
        }

        // POST: /api/QuizResult/submit
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuizResult(
            [FromQuery] Guid userId,
            [FromQuery] Guid quizId,
            [FromBody] List<QuizResultAnswerDto> answers)
        {
            if (answers == null || !answers.Any())
                return BadRequest("No answers submitted.");

            try
            {
                var result = await _quizResultService.SubmitQuizResult(userId, quizId, answers);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: /api/QuizResult/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserQuizResults(Guid userId)
        {
            var results = await _quizResultService.GetUserQuizResults(userId);
            if (results == null || results.Count == 0)
                return NotFound("No quiz results found for this user.");

            return Ok(results);
        }

        // GET: /api/QuizResult/{resultId}?userId=1
        [HttpGet("{resultId}")]
        public async Task<IActionResult> GetQuizResult(Guid resultId, [FromQuery] Guid userId)
        {
            var result = await _quizResultService.GetQuizResult(resultId, userId);
            if (result == null)
                return NotFound("Quiz result not found or access denied.");

            return Ok(result);
        }
    }
}
