using ProductivIO.Backend.DTOs.Quiz;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IQuizResultService
    {
        Task<QuizResultDto> SubmitQuizResult(SubmitQuizResultDto submission, Guid userId);
        Task<List<QuizResultDto>> GetUserQuizResults(Guid userId);
        Task<QuizResultDto?> GetQuizResult(Guid resultId, Guid userId);
    }
}
