using ProductivIOBackend.DTOs.Quiz;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface IQuizResultService
    {
        Task<QuizResultDto> SubmitQuizResult(Guid  userId, Guid  quizId, List<QuizResultAnswerDto> answers);
        Task<List<QuizResultDto>> GetUserQuizResults(Guid  userId);
        Task<QuizResultDto?> GetQuizResult(Guid  resultId, Guid  userId);
    }
}
