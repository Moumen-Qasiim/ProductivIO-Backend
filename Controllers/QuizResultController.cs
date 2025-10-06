using Microsoft.AspNetCore.Mvc;
using ProductivIOBackend.Services.Interfaces;
using ProductivIOBackend.DTOs.Quiz;

namespace ProductivIOBackend.Controllers
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
            [FromQuery] int userId,
            [FromQuery] int quizId,
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
        public async Task<IActionResult> GetUserQuizResults(int userId)
        {
            var results = await _quizResultService.GetUserQuizResults(userId);
            if (results == null || results.Count == 0)
                return NotFound("No quiz results found for this user.");

            return Ok(results);
        }

        // GET: /api/QuizResult/{resultId}?userId=1
        [HttpGet("{resultId}")]
        public async Task<IActionResult> GetQuizResult(int resultId, [FromQuery] int userId)
        {
            var result = await _quizResultService.GetQuizResult(resultId, userId);
            if (result == null)
                return NotFound("Quiz result not found or access denied.");

            return Ok(result);
        }
    }
}
