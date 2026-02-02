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
    public class QuizResultController : ControllerBase
    {
        private readonly IQuizResultService _quizResultService;

        public QuizResultController(IQuizResultService quizResultService)
        {
            _quizResultService = quizResultService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Guid.Empty;
            return Guid.Parse(userIdClaim.Value);
        }

        // POST: /api/QuizResult/submit
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuizResult([FromBody] SubmitQuizResultDto submission)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = GetUserId();
                var result = await _quizResultService.SubmitQuizResult(submission, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: /api/QuizResult
        [HttpGet]
        public async Task<IActionResult> GetUserQuizResults()
        {
            var userId = GetUserId();
            var results = await _quizResultService.GetUserQuizResults(userId);
            return Ok(results);
        }

        // GET: /api/QuizResult/{resultId}
        [HttpGet("{resultId}")]
        public async Task<IActionResult> GetQuizResult(Guid resultId)
        {
            var userId = GetUserId();
            var result = await _quizResultService.GetQuizResult(resultId, userId);
            if (result == null)
                return NotFound(new { message = "Quiz result not found or access denied." });

            return Ok(result);
        }
    }
}
