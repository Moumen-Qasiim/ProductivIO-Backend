using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.DTOs.Quiz;
using ProductivIO.Backend.Repositories.Interfaces;

namespace ProductivIO.Backend.Repositories
{
    public class QuizResultRepository : IQuizResultRepository
    {
        private readonly AppDbContext _db;

        public QuizResultRepository(AppDbContext db)
        {
            _db = db;
        }

        // Add a new quiz result
        public async Task<QuizResultDto> AddQuizResultAsync(QuizResultDto resultDto)
        {
            // Map DTO to model
            var result = new Models.QuizResult
            {
                QuizId = resultDto.QuizId,
                UserId = resultDto.UserId,
                Score = resultDto.Score,
                TotalQuestions = resultDto.TotalQuestions,
                CorrectAnswers = resultDto.CorrectAnswers,
                CreatedAt = resultDto.TakenAt,
                ResultAnswers = resultDto.Answers.Select(a => new Models.QuizResultAnswer
                {
                    QuestionId = a.QuestionId,
                    AnswerId = a.AnswerId,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };

            _db.QuizResults.Add(result);
            await _db.SaveChangesAsync();

            // Map back to DTO
            resultDto.Id = result.Id;
            return resultDto;
        }

        // Get all results for a user
        public async Task<List<QuizResultDto>> GetResultsByUserAsync(Guid userId)
        {
            var results = await _db.QuizResults
                .Include(r => r.ResultAnswers)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return results.Select(r => new QuizResultDto
            {
                Id = r.Id,
                QuizId = r.QuizId,
                UserId = r.UserId,
                Score = r.Score,
                TotalQuestions = r.TotalQuestions,
                CorrectAnswers = r.CorrectAnswers,
                TakenAt = r.CreatedAt,
                Answers = r.ResultAnswers.Select(a => new QuizResultAnswerDto
                {
                    QuestionId = a.QuestionId,
                    AnswerId = a.AnswerId,
                    IsCorrect = a.IsCorrect
                }).ToList()
            }).ToList();
        }

        // Get a single result by ID for a user
        public async Task<QuizResultDto?> GetResultByIdAsync(Guid resultId, Guid userId)
        {
            var result = await _db.QuizResults
                .Include(r => r.ResultAnswers)
                .FirstOrDefaultAsync(r => r.Id == resultId && r.UserId == userId);

            if (result == null) return null;

            return new QuizResultDto
            {
                Id = result.Id,
                QuizId = result.QuizId,
                UserId = result.UserId,
                Score = result.Score,
                TotalQuestions = result.TotalQuestions,
                CorrectAnswers = result.CorrectAnswers,
                TakenAt = result.CreatedAt,
                Answers = result.ResultAnswers.Select(a => new QuizResultAnswerDto
                {
                    QuestionId = a.QuestionId,
                    AnswerId = a.AnswerId,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
        }
    }
}
