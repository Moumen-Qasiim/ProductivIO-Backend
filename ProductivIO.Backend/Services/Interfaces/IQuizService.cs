using ProductivIO.Backend.DTOs.Quiz;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IQuizService
    {
        Task<List<QuizzesDto>> GetAllQuizzes(Guid userId);
        Task<QuizzesDto?> GetQuiz(Guid id, Guid userId);
        Task<QuizzesDto?> AddQuiz(CreateQuizDto quiz, Guid userId);
        Task<bool> UpdateQuiz(Guid id, UpdateQuizDto quiz, Guid userId);
        Task<bool> DeleteQuiz(Guid id, Guid userId);

        Task<QuizQuestionDto?> AddQuestion(Guid quizId, CreateQuizQuestionDto question, Guid userId);
        Task<bool> UpdateQuestion(Guid questionId, UpdateQuizQuestionDto question, Guid userId);
        Task<bool> DeleteQuestion(Guid questionId, Guid userId);

        Task<QuizAnswerDto?> AddAnswer(Guid questionId, CreateQuizAnswerDto answer, Guid userId);
        Task<bool> UpdateAnswer(Guid answerId, UpdateQuizAnswerDto answer, Guid userId);
        Task<bool> DeleteAnswer(Guid answerId, Guid userId);
    }
}