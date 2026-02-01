using Microsoft.EntityFrameworkCore;
using ProductivIOBackend.Data;
using ProductivIOBackend.DTOs.Quiz;
using ProductivIOBackend.Models; 
using ProductivIOBackend.Repositories.Interfaces;

namespace ProductivIOBackend.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _db;

        public QuizRepository(AppDbContext db)
        {
            _db = db;
        }

        // Quizzes
        public async Task<List<QuizzesDto>> GetAllQuizzesAsync(Guid UserId)
        {
            var quizzes = await _db.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .Where(q => q.UserId == UserId)
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

        public async Task<QuizzesDto?> GetQuizAsync(Guid QuizId, Guid UserId)
        {
            var quiz = await _db.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .FirstOrDefaultAsync(q => q.Id == QuizId && q.UserId == UserId);

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

        public async Task<QuizzesDto> AddQuizAsync(QuizzesDto quizDto)
        {
            var quiz = new Quizzes
            {
                UserId = quizDto.UserId,
                Title = quizDto.Title,
                Description = quizDto.Description,
                CreatedAt = DateTime.Now
            };

            _db.Quizzes.Add(quiz);
            await _db.SaveChangesAsync();

            quizDto.Id = quiz.Id;
            quizDto.CreatedAt = quiz.CreatedAt;
            return quizDto;
        }

        public async Task<QuizzesDto?> UpdateQuizAsync(QuizzesDto quizDto)
        {
            var quiz = await _db.Quizzes.FindAsync(quizDto.Id);
            if (quiz == null) return null;

            quiz.Title = quizDto.Title;
            quiz.Description = quizDto.Description;
            quiz.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            quizDto.UpdatedAt = quiz.UpdatedAt;
            return quizDto;
        }

        public async Task<bool> DeleteQuizAsync(Guid QuizId, Guid UserId)
        {
            var quiz = await _db.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .FirstOrDefaultAsync(q => q.Id == QuizId && q.UserId == UserId);

            if (quiz == null) return false;

            _db.Quizzes.Remove(quiz);
            await _db.SaveChangesAsync();
            return true;
        }

        // Quiz Questions
        public async Task<QuizQuestionDto?> AddQuestionAsync(Guid QuizId, QuizQuestionDto questionDto)
        {
            var quiz = await _db.Quizzes.FindAsync(QuizId);
            if (quiz == null) return null;

            var question = new QuizQuestion
            {
                QuizId = QuizId,
                Question = questionDto.Question,
                CreatedAt = DateTime.Now
            };

            _db.QuizQuestions.Add(question);
            await _db.SaveChangesAsync();

            questionDto.Id = question.Id;
            questionDto.QuizId = question.QuizId;
            questionDto.CreatedAt = question.CreatedAt;
            return questionDto;
        }

        public async Task<QuizQuestionDto?> UpdateQuestionAsync(QuizQuestionDto questionDto)
        {
            var question = await _db.QuizQuestions.FindAsync(questionDto.Id);
            if (question == null) return null;

            question.Question = questionDto.Question;
            question.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            questionDto.UpdatedAt = question.UpdatedAt;
            return questionDto;
        }

        public async Task<bool> DeleteQuestionAsync(Guid questionId)
        {
            var question = await _db.QuizQuestions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null) return false;

            _db.QuizQuestions.Remove(question);
            await _db.SaveChangesAsync();
            return true;
        }

        // Quiz Answers
        public async Task<QuizAnswerDto?> AddAnswerAsync(Guid questionId, QuizAnswerDto answerDto)
        {
            var question = await _db.QuizQuestions.FindAsync(questionId);
            if (question == null) return null;

            var answer = new QuizAnswer
            {
                QuestionId = questionId,
                Answer = answerDto.Answer,
                IsCorrect = answerDto.IsCorrect,
                CreatedAt = DateTime.Now
            };

            _db.QuizAnswers.Add(answer);
            await _db.SaveChangesAsync();

            answerDto.Id = answer.Id;
            answerDto.CreatedAt = answer.CreatedAt;
            return answerDto;
        }

        public async Task<QuizAnswerDto?> UpdateAnswerAsync(QuizAnswerDto answerDto)
        {
            var answer = await _db.QuizAnswers.FindAsync(answerDto.Id);
            if (answer == null) return null;

            answer.Answer = answerDto.Answer;
            answer.IsCorrect = answerDto.IsCorrect;
            answer.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            answerDto.UpdatedAt = answer.UpdatedAt;
            return answerDto;
        }

        public async Task<bool> DeleteAnswerAsync(Guid answerId)
        {
            var answer = await _db.QuizAnswers.FindAsync(answerId);
            if (answer == null) return false;

            _db.QuizAnswers.Remove(answer);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
