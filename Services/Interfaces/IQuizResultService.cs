using ProductivIOBackend.DTOs.Quiz;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface IQuizResultService
    {
        Task<QuizResultDto> SubmitQuizResult(int userId, int quizId, List<QuizResultAnswerDto> answers);
        Task<List<QuizResultDto>> GetUserQuizResults(int userId);
        Task<QuizResultDto?> GetQuizResult(int resultId, int userId);
    }
}
