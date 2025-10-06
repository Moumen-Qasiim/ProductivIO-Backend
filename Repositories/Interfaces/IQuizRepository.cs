using ProductivIOBackend.DTOs.Quiz;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        // Quizzes
        Task<List<QuizzesDto>> GetAllQuizzesAsync(int userId);
        Task<QuizzesDto?> GetQuizAsync(int quizId, int userId);
        Task<QuizzesDto> AddQuizAsync(QuizzesDto quiz);
        Task<QuizzesDto?> UpdateQuizAsync(QuizzesDto quiz);
        Task<bool> DeleteQuizAsync(int quizId, int userId);

        // Questions
        Task<QuizQuestionDto?> AddQuestionAsync(int quizId, QuizQuestionDto question);
        Task<QuizQuestionDto?> UpdateQuestionAsync(QuizQuestionDto question);
        Task<bool> DeleteQuestionAsync(int questionId);

        // Answers
        Task<QuizAnswerDto?> AddAnswerAsync(int questionId, QuizAnswerDto answer);
        Task<QuizAnswerDto?> UpdateAnswerAsync(QuizAnswerDto answer);
        Task<bool> DeleteAnswerAsync(int answerId);
    }
}
