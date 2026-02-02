using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.DTOs.Quiz;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Repositories.Interfaces;

namespace ProductivIO.Backend.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _db;

        public QuizRepository(AppDbContext db)
        {
            _db = db;
        }

        // Quizzes
        public async Task<List<QuizzesDto>> GetAllQuizzesAsync(Guid userId)
        {
            var quizzes = await _db.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .Where(q => q.UserId == userId)
                .ToListAsync();

            return quizzes.Select(q => new QuizzesDto
            {
                Id = q.Id,
                UserId = q.UserId,
                Title = q.Title,
                Description = q.Description,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                Questions = q.QuizQuestions.Select(qq => new QuizQuestionDto
                {
                    Id = qq.Id,
                    QuizId = qq.QuizId,
                    Question = qq.Question,
                    CreatedAt = qq.CreatedAt,
                    UpdatedAt = qq.UpdatedAt,
                    Answers = qq.Answers.Select(a => new QuizAnswerDto
                    {
                        Id = a.Id,
                        QuestionId = a.QuestionId,
                        Answer = a.Answer,
                        IsCorrect = a.IsCorrect,
                        CreatedAt = a.CreatedAt,
                        UpdatedAt = a.UpdatedAt
                    }).ToList()
                }).ToList()
            }).ToList();
        }

        public async Task<QuizzesDto?> GetQuizAsync(Guid quizId, Guid userId)
        {
            var quiz = await _db.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .FirstOrDefaultAsync(q => q.Id == quizId && q.UserId == userId);

            if (quiz == null) return null;

            return new QuizzesDto
            {
                Id = quiz.Id,
                UserId = quiz.UserId,
                Title = quiz.Title,
                Description = quiz.Description,
                CreatedAt = quiz.CreatedAt,
                UpdatedAt = quiz.UpdatedAt,
                Questions = quiz.QuizQuestions.Select(qq => new QuizQuestionDto
                {
                    Id = qq.Id,
                    QuizId = qq.QuizId,
                    Question = qq.Question,
                    CreatedAt = qq.CreatedAt,
                    UpdatedAt = qq.UpdatedAt,
                    Answers = qq.Answers.Select(a => new QuizAnswerDto
                    {
                        Id = a.Id,
                        QuestionId = a.QuestionId,
                        Answer = a.Answer,
                        IsCorrect = a.IsCorrect,
                        CreatedAt = a.CreatedAt,
                        UpdatedAt = a.UpdatedAt
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<QuizzesDto?> AddQuizAsync(CreateQuizDto dto, Guid userId)
        {
            var quiz = new Quizzes
            {
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description
            };

            _db.Quizzes.Add(quiz);
            await _db.SaveChangesAsync();

            return new QuizzesDto
            {
                Id = quiz.Id,
                UserId = quiz.UserId,
                Title = quiz.Title,
                Description = quiz.Description,
                CreatedAt = quiz.CreatedAt,
                UpdatedAt = quiz.UpdatedAt
            };
        }

        public async Task<QuizzesDto?> UpdateQuizAsync(Guid id, UpdateQuizDto dto, Guid userId)
        {
            var existing = await _db.Quizzes
                .FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Description = dto.Description;

            await _db.SaveChangesAsync();

            return new QuizzesDto
            {
                Id = existing.Id,
                UserId = existing.UserId,
                Title = existing.Title,
                Description = existing.Description,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteQuizAsync(Guid quizId, Guid userId)
        {
            var quiz = await _db.Quizzes
                .FirstOrDefaultAsync(q => q.Id == quizId && q.UserId == userId);

            if (quiz == null) return false;

            _db.Quizzes.Remove(quiz);
            await _db.SaveChangesAsync();
            return true;
        }

        // Questions
        public async Task<QuizQuestionDto?> AddQuestionAsync(Guid quizId, CreateQuizQuestionDto dto, Guid userId)
        {
            var quiz = await _db.Quizzes.AnyAsync(q => q.Id == quizId && q.UserId == userId);
            if (!quiz) return null;

            var entity = new QuizQuestion
            {
                QuizId = quizId,
                Question = dto.Question
            };

            _db.QuizQuestions.Add(entity);
            await _db.SaveChangesAsync();

            return new QuizQuestionDto
            {
                Id = entity.Id,
                QuizId = entity.QuizId,
                Question = entity.Question,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<QuizQuestionDto?> UpdateQuestionAsync(Guid questionId, UpdateQuizQuestionDto dto, Guid userId)
        {
            var existing = await _db.QuizQuestions
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(q => q.Id == questionId && q.Quiz!.UserId == userId);

            if (existing == null) return null;

            existing.Question = dto.Question;

            await _db.SaveChangesAsync();

            return new QuizQuestionDto
            {
                Id = existing.Id,
                QuizId = existing.QuizId,
                Question = existing.Question,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId)
        {
            var question = await _db.QuizQuestions
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(q => q.Id == questionId && q.Quiz!.UserId == userId);

            if (question == null) return false;

            _db.QuizQuestions.Remove(question);
            await _db.SaveChangesAsync();
            return true;
        }

        // Answers
        public async Task<QuizAnswerDto?> AddAnswerAsync(Guid questionId, CreateQuizAnswerDto dto, Guid userId)
        {
            var question = await _db.QuizQuestions
                .Include(q => q.Quiz)
                .AnyAsync(q => q.Id == questionId && q.Quiz!.UserId == userId);
            if (!question) return null;

            var entity = new QuizAnswer
            {
                QuestionId = questionId,
                Answer = dto.Answer,
                IsCorrect = dto.IsCorrect
            };

            _db.QuizAnswers.Add(entity);
            await _db.SaveChangesAsync();

            return new QuizAnswerDto
            {
                Id = entity.Id,
                QuestionId = entity.QuestionId,
                Answer = entity.Answer,
                IsCorrect = entity.IsCorrect,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<QuizAnswerDto?> UpdateAnswerAsync(Guid answerId, UpdateQuizAnswerDto dto, Guid userId)
        {
            var existing = await _db.QuizAnswers
                .Include(a => a.QuizQuestion)
                    .ThenInclude(q => q!.Quiz)
                .FirstOrDefaultAsync(a => a.Id == answerId && a.QuizQuestion!.Quiz!.UserId == userId);

            if (existing == null) return null;

            existing.Answer = dto.Answer;
            existing.IsCorrect = dto.IsCorrect;

            await _db.SaveChangesAsync();

            return new QuizAnswerDto
            {
                Id = existing.Id,
                QuestionId = existing.QuestionId,
                Answer = existing.Answer,
                IsCorrect = existing.IsCorrect,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId)
        {
            var answer = await _db.QuizAnswers
                .Include(a => a.QuizQuestion)
                    .ThenInclude(q => q!.Quiz)
                .FirstOrDefaultAsync(a => a.Id == answerId && a.QuizQuestion!.Quiz!.UserId == userId);

            if (answer == null) return false;

            _db.QuizAnswers.Remove(answer);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
