using ProductivIO.Backend.DTOs.Quiz;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IQuizResultService
    {
        Task<QuizResultDto> SubmitQuizResult(Guid  userId, Guid  quizId, List<QuizResultAnswerDto> answers);
        Task<List<QuizResultDto>> GetUserQuizResults(Guid  userId);
        Task<QuizResultDto?> GetQuizResult(Guid  resultId, Guid  userId);
    }
}
