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

        public async Task<QuizResultDto> SubmitQuizResult(SubmitQuizResultDto submission, Guid userId)
        {
            // Load the quiz with its questions and answers, scoped by userId
            var quiz = await _quizRepository.GetQuizAsync(submission.QuizId, userId);
            if (quiz == null)
                throw new Exception("Quiz not found or you don't have access to it.");

            var totalQuestions = quiz.Questions.Count;
            int correctAnswers = 0;
            var resultAnswers = new List<QuizResultAnswerDto>();

            // Check submitted answers against correct ones
            foreach (var submitted in submission.Answers)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.Id == submitted.QuestionId);
                if (question == null) continue;

                var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == submitted.AnswerId);
                if (selectedAnswer == null) continue;

                bool isCorrect = selectedAnswer.IsCorrect;
                if (isCorrect) correctAnswers++;

                resultAnswers.Add(new QuizResultAnswerDto
                {
                    QuestionId = submitted.QuestionId,
                    AnswerId = submitted.AnswerId,
                    IsCorrect = isCorrect
                });
            }

            // Create QuizResult DTO for repository
            var resultDto = new QuizResultDto
            {
                QuizId = submission.QuizId,
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers,
                Score = totalQuestions > 0 ? (int)((double)correctAnswers / totalQuestions * 100) : 0,
                Answers = resultAnswers
            };

            return await _quizResultRepository.AddQuizResultAsync(resultDto, userId);
        }

        public async Task<List<QuizResultDto>> GetUserQuizResults(Guid userId)
        {
            return await _quizResultRepository.GetResultsByUserAsync(userId);
        }

        public async Task<QuizResultDto?> GetQuizResult(Guid resultId, Guid userId)
        {
            return await _quizResultRepository.GetResultByIdAsync(resultId, userId);
        }
    }
}
