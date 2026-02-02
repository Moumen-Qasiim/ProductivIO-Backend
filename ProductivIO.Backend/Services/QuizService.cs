using ProductivIO.Backend.DTOs.Quiz;
using ProductivIO.Backend.Repositories.Interfaces;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<List<QuizzesDto>> GetAllQuizzes(Guid userId)
        {
            return await _quizRepository.GetAllQuizzesAsync(userId);
        }

        public async Task<QuizzesDto?> GetQuiz(Guid id, Guid userId)
        {
            return await _quizRepository.GetQuizAsync(id, userId);
        }

        public async Task<QuizzesDto?> AddQuiz(CreateQuizDto quiz, Guid userId)
        {
            return await _quizRepository.AddQuizAsync(quiz, userId);
        }

        public async Task<bool> UpdateQuiz(Guid id, UpdateQuizDto quiz, Guid userId)
        {
            var updated = await _quizRepository.UpdateQuizAsync(id, quiz, userId);
            return updated != null;
        }

        public async Task<bool> DeleteQuiz(Guid id, Guid userId)
        {
            return await _quizRepository.DeleteQuizAsync(id, userId);
        }

        public async Task<QuizQuestionDto?> AddQuestion(Guid quizId, CreateQuizQuestionDto question, Guid userId)
        {
            return await _quizRepository.AddQuestionAsync(quizId, question, userId);
        }

        public async Task<bool> UpdateQuestion(Guid questionId, UpdateQuizQuestionDto question, Guid userId)
        {
            var updated = await _quizRepository.UpdateQuestionAsync(questionId, question, userId);
            return updated != null;
        }

        public async Task<bool> DeleteQuestion(Guid questionId, Guid userId)
        {
            return await _quizRepository.DeleteQuestionAsync(questionId, userId);
        }

        public async Task<QuizAnswerDto?> AddAnswer(Guid questionId, CreateQuizAnswerDto answer, Guid userId)
        {
            return await _quizRepository.AddAnswerAsync(questionId, answer, userId);
        }

        public async Task<bool> UpdateAnswer(Guid answerId, UpdateQuizAnswerDto answer, Guid userId)
        {
            var updated = await _quizRepository.UpdateAnswerAsync(answerId, answer, userId);
            return updated != null;
        }

        public async Task<bool> DeleteAnswer(Guid answerId, Guid userId)
        {
            return await _quizRepository.DeleteAnswerAsync(answerId, userId);
        }
    }
}