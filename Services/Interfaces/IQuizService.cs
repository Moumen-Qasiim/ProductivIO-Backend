using ProductivIOBackend.DTOs.Quiz;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface IQuizService
    {
        Task<List<QuizzesDto>> GetAllQuizzes(int userId);
        Task<QuizzesDto?> GetQuiz(int id, int userId);
        Task<QuizzesDto?> AddQuiz(QuizzesDto quiz);
        Task<bool> UpdateQuiz(int id, QuizzesDto quiz);
        Task<bool> DeleteQuiz(int id, int userId);
        Task<QuizQuestionDto?> AddQuestion(int quizId, QuizQuestionDto question);
        Task<bool> UpdateQuestion(int quizId, QuizQuestionDto question);
        Task<bool> DeleteQuestion(int questionId);
        Task<QuizAnswerDto?> AddAnswer(int questionId, QuizAnswerDto answer);
        Task<bool> UpdateAnswer(int questionId, QuizAnswerDto answer);
        Task<bool> DeleteAnswer(int answerId);
    }
}