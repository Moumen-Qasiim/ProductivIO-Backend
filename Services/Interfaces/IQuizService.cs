using ProductivIOBackend.DTOs.Quiz;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface IQuizService
    {
        Task<List<QuizzesDto>> GetAllQuizzes(Guid  userId);
        Task<QuizzesDto?> GetQuiz(Guid  id, Guid   userId);
        Task<QuizzesDto?> AddQuiz(QuizzesDto quiz);
        Task<bool> UpdateQuiz(Guid  id, QuizzesDto quiz);
        Task<bool> DeleteQuiz(Guid  id, Guid   userId);
        Task<QuizQuestionDto?> AddQuestion(Guid  quizId, QuizQuestionDto question);
        Task<bool> UpdateQuestion(Guid  quizId, QuizQuestionDto question);
        Task<bool> DeleteQuestion(Guid  questionId);
        Task<QuizAnswerDto?> AddAnswer(Guid  questionId, QuizAnswerDto answer);
        Task<bool> UpdateAnswer(Guid  questionId, QuizAnswerDto answer);
        Task<bool> DeleteAnswer(Guid  answerId);
    }
}