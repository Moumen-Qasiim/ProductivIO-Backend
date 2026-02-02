using ProductivIO.Backend.DTOs.Quiz;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        // Quizzes
        Task<List<QuizzesDto>> GetAllQuizzesAsync(Guid userId);
        Task<QuizzesDto?> GetQuizAsync(Guid quizId, Guid userId);
        Task<QuizzesDto?> AddQuizAsync(CreateQuizDto quiz, Guid userId);
        Task<QuizzesDto?> UpdateQuizAsync(Guid id, UpdateQuizDto quiz, Guid userId);
        Task<bool> DeleteQuizAsync(Guid quizId, Guid userId);

        // Questions
        Task<QuizQuestionDto?> AddQuestionAsync(Guid quizId, CreateQuizQuestionDto question, Guid userId);
        Task<QuizQuestionDto?> UpdateQuestionAsync(Guid questionId, UpdateQuizQuestionDto question, Guid userId);
        Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId);

        // Answers
        Task<QuizAnswerDto?> AddAnswerAsync(Guid questionId, CreateQuizAnswerDto answer, Guid userId);
        Task<QuizAnswerDto?> UpdateAnswerAsync(Guid answerId, UpdateQuizAnswerDto answer, Guid userId);
        Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId);
    }
}
