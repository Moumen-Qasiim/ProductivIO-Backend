using System.Threading.Tasks;
using ProductivIOBackend.DTOs.Quiz;
using ProductivIOBackend.Models;
using ProductivIOBackend.Repositories.Interfaces;
using ProductivIOBackend.Services.Interfaces;

namespace ProductivIOBackend.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        // Quizzes
        public async Task<List<QuizzesDto>> GetAllQuizzes(Guid UserId)
        {
            var quizzes = await _quizRepository.GetAllQuizzesAsync(UserId);

            return quizzes.Select(q => new QuizzesDto
            {
                Id = q.Id,
                UserId = q.UserId,
                Title = q.Title,
                Description = q.Description,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,

                Questions = q.Questions.Select(qq => new QuizQuestionDto
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

        public async Task<QuizzesDto?> GetQuiz(Guid id, Guid UserId)
        {
            var quiz = await _quizRepository.GetQuizAsync(id, UserId);
            if (quiz == null) return null;


            return new QuizzesDto
            {
                Id = quiz.Id,
                UserId = quiz.UserId,
                Title = quiz.Title,
                Description = quiz.Description,
                CreatedAt = quiz.CreatedAt,
                UpdatedAt = quiz.UpdatedAt,

                Questions = quiz.Questions.Select(qq => new QuizQuestionDto
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

        public async Task<QuizzesDto?> AddQuiz(QuizzesDto quiz)
        {
            var created = await _quizRepository.AddQuizAsync(quiz);
            if (created == null) return null;

            return new QuizzesDto
            {
                Id = created.Id,
                UserId = created.UserId,
                Title = created.Title,
                Description = created.Description,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt,

                Questions = created.Questions.Select(qq => new QuizQuestionDto
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

        public async Task<bool> UpdateQuiz(Guid id, QuizzesDto quiz)
        {
            if (id != quiz.Id) return false;

            var updated = await _quizRepository.UpdateQuizAsync(quiz);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> DeleteQuiz(Guid id, Guid UserId)
        {
            return await _quizRepository.DeleteQuizAsync(id, UserId);
        }


        // Quiz Question
        public async Task<QuizQuestionDto?> AddQuestion(Guid QuizId, QuizQuestionDto question)
        {
            var created = await _quizRepository.AddQuestionAsync(QuizId, question);
            if (created == null) return null;

            return new QuizQuestionDto
            {
                Id = created.Id,
                QuizId = created.QuizId,
                Question = created.Question,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt,

                Answers = created.Answers.Select(a => new QuizAnswerDto
                {
                    Id = a.Id,
                    QuestionId = a.QuestionId,
                    Answer = a.Answer,
                    IsCorrect = a.IsCorrect,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                }).ToList()
            };
        }

        public async Task<bool> UpdateQuestion(Guid QuizId, QuizQuestionDto question)
        {
            if (QuizId != question.QuizId) return false;

            var updated = await _quizRepository.UpdateQuestionAsync(question);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> DeleteQuestion(Guid questionId)
        {
            return await _quizRepository.DeleteQuestionAsync(questionId);
        }

        // Quiz Answer
        public async Task<QuizAnswerDto?> AddAnswer(Guid questionId, QuizAnswerDto answer)
        {
            var created = await _quizRepository.AddAnswerAsync(questionId, answer);
            if (created == null) return null;

            return new QuizAnswerDto
            {
                Id = created.Id,
                QuestionId = created.QuestionId,
                Answer = created.Answer,
                IsCorrect = created.IsCorrect,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
            
        }

        public async Task<bool> UpdateAnswer(Guid questionId, QuizAnswerDto answer)
        {
            if (questionId != answer.QuestionId) return false;

            var updated = await _quizRepository.UpdateAnswerAsync(answer);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> DeleteAnswer(Guid answerId)
        {
            return await _quizRepository.DeleteAnswerAsync(answerId);
        }
    }
}