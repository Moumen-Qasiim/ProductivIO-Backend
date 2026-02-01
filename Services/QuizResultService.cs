using ProductivIO.Backend.DTOs.Quiz;
using ProductivIO.Backend.Repositories.Interfaces;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class QuizResultService : IQuizResultService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizResultRepository _quizResultRepository;

        public QuizResultService(IQuizRepository quizRepository, IQuizResultRepository quizResultRepository)
        {
            _quizRepository = quizRepository;
            _quizResultRepository = quizResultRepository;
        }

        public async Task<QuizResultDto> SubmitQuizResult(Guid userId, Guid quizId, List<QuizResultAnswerDto> answers)
        {
            // Load the quiz with its questions and answers
            var quiz = await _quizRepository.GetQuizAsync(quizId, userId);
            if (quiz == null)
                throw new Exception("Quiz not found or you don't have access to it.");

            var totalQuestions = quiz.Questions.Count;
            int correctAnswers = 0;
            var resultAnswers = new List<QuizResultAnswerDto>();

            // Check submitted answers against correct ones
            foreach (var answer in answers)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null) continue;

                var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == answer.AnswerId);
                if (selectedAnswer == null) continue;

                bool isCorrect = selectedAnswer.IsCorrect;
                if (isCorrect) correctAnswers++;

                resultAnswers.Add(new QuizResultAnswerDto
                {
                    QuestionId = answer.QuestionId,
                    AnswerId = answer.AnswerId,
                    IsCorrect = isCorrect
                });
            }

            // Create and save QuizResult
            var result = new QuizResultDto
            {
                UserId = userId,
                QuizId = quizId,
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers,
                Score = (int)((double)correctAnswers / totalQuestions * 100),
                Answers = resultAnswers
            };

            var savedResult = await _quizResultRepository.AddQuizResultAsync(result);

            // Map to DTO
            return new QuizResultDto
            {
                Id = savedResult.Id,
                QuizId = quizId,
                UserId = userId,
                Score = savedResult.Score,
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers,
                TakenAt = savedResult.TakenAt,
                Answers = resultAnswers.Select(a => new QuizResultAnswerDto
                {
                    QuestionId = a.QuestionId,
                    AnswerId = a.AnswerId,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
        }

        public async Task<List<QuizResultDto>> GetUserQuizResults(Guid userId)
        {
            var results = await _quizResultRepository.GetResultsByUserAsync(userId);
            return results.Select(r => new QuizResultDto
            {
                Id = r.Id,
                QuizId = r.QuizId,
                UserId = r.UserId,
                Score = r.Score,
                TotalQuestions = r.TotalQuestions,
                CorrectAnswers = r.CorrectAnswers,
                TakenAt = r.TakenAt
            }).ToList();
        }

        public async Task<QuizResultDto?> GetQuizResult(Guid resultId, Guid userId)
        {
            var result = await _quizResultRepository.GetResultByIdAsync(resultId, userId);
            if (result == null) return null;

            return new QuizResultDto
            {
                Id = result.Id,
                QuizId = result.QuizId,
                UserId = result.UserId,
                Score = result.Score,
                TotalQuestions = result.TotalQuestions,
                CorrectAnswers = result.CorrectAnswers,
                TakenAt = result.TakenAt,
                Answers = result.Answers.Select(a => new QuizResultAnswerDto
                {
                    QuestionId = a.QuestionId,
                    AnswerId = a.AnswerId,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
        }
    }
}
