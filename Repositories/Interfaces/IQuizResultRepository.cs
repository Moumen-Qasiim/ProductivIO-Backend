using ProductivIO.Backend.DTOs.Quiz;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface IQuizResultRepository
    {
        Task<QuizResultDto> AddQuizResultAsync(QuizResultDto result);
        Task<List<QuizResultDto>> GetResultsByUserAsync(Guid userId);
        Task<QuizResultDto?> GetResultByIdAsync(Guid resultId, Guid userId);
    }

}