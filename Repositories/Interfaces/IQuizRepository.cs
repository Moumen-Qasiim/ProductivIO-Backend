using ProductivIO.Backend.DTOs.Quiz;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        // Quizzes
        Task<List<QuizzesDto>> GetAllQuizzesAsync(Guid userId);
        Task<QuizzesDto?> GetQuizAsync(Guid quizId, Guid userId);
        Task<QuizzesDto> AddQuizAsync(QuizzesDto quiz);
        Task<QuizzesDto?> UpdateQuizAsync(QuizzesDto quiz);
        Task<bool> DeleteQuizAsync(Guid quizId, Guid userId);

        // Questions
        Task<QuizQuestionDto?> AddQuestionAsync(Guid quizId, QuizQuestionDto question);
        Task<QuizQuestionDto?> UpdateQuestionAsync(QuizQuestionDto question);
        Task<bool> DeleteQuestionAsync(Guid questionId);

        // Answers
        Task<QuizAnswerDto?> AddAnswerAsync(Guid questionId, QuizAnswerDto answer);
        Task<QuizAnswerDto?> UpdateAnswerAsync(QuizAnswerDto answer);
        Task<bool> DeleteAnswerAsync(Guid answerId);
    }
}
