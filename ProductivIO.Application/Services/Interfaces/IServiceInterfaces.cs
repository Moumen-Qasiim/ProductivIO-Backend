using ProductivIO.Contracts.Requests.Notes;
using ProductivIO.Contracts.Responses.Notes;
using ProductivIO.Contracts.Requests.Tasks;
using ProductivIO.Contracts.Responses.Tasks;
using ProductivIO.Contracts.Requests.Flashcards;
using ProductivIO.Contracts.Responses.Flashcards;
using ProductivIO.Contracts.Requests.Quiz;
using ProductivIO.Contracts.Responses.Quiz;
using ProductivIO.Contracts.Requests.Pomodoro;
using ProductivIO.Contracts.Responses.Pomodoro;
using ProductivIO.Contracts.Requests.Auth;
using ProductivIO.Contracts.Responses.Auth;
using System.Security.Claims;

namespace ProductivIO.Application.Services.Interfaces;

public interface INoteService
{
    Task<IEnumerable<NoteResponse>> GetAllAsync(Guid userId);
    Task<NoteResponse?> GetByIdAsync(Guid id, Guid userId);
    Task<NoteResponse?> CreateAsync(CreateNoteRequest request, Guid userId);
    Task<bool> UpdateAsync(Guid id, UpdateNoteRequest request, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetAllAsync(Guid userId);
    Task<TaskResponse?> GetByIdAsync(Guid id, Guid userId);
    Task<TaskResponse?> CreateAsync(CreateTaskRequest request, Guid userId);
    Task<bool> UpdateAsync(Guid id, UpdateTaskRequest request, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}

public interface IFlashcardService
{
    Task<IEnumerable<FlashcardResponse>> GetAllAsync(Guid userId);
    Task<FlashcardResponse?> GetByIdAsync(Guid id, Guid userId);
    Task<FlashcardResponse?> CreateAsync(CreateFlashcardRequest request, Guid userId);
    Task<bool> UpdateAsync(Guid id, UpdateFlashcardRequest request, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);

    Task<FlashcardQuestionResponse?> AddQuestionAsync(Guid flashcardId, CreateFlashcardQuestionRequest request, Guid userId);
    Task<bool> UpdateQuestionAsync(Guid questionId, UpdateFlashcardQuestionRequest request, Guid userId);
    Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId);

    Task<FlashcardAnswerResponse?> AddAnswerAsync(Guid questionId, CreateFlashcardAnswerRequest request, Guid userId);
    Task<bool> UpdateAnswerAsync(Guid answerId, UpdateFlashcardAnswerRequest request, Guid userId);
    Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId);
}

public interface IQuizService
{
    Task<IEnumerable<QuizResponse>> GetAllAsync(Guid userId);
    Task<QuizResponse?> GetByIdAsync(Guid id, Guid userId);
    Task<QuizResponse?> CreateAsync(CreateQuizRequest request, Guid userId);
    Task<bool> UpdateAsync(Guid id, UpdateQuizRequest request, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);

    Task<QuizQuestionResponse?> AddQuestionAsync(Guid quizId, CreateQuizQuestionRequest request, Guid userId);
    Task<bool> UpdateQuestionAsync(Guid questionId, UpdateQuizQuestionRequest request, Guid userId);
    Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId);

    Task<QuizAnswerResponse?> AddAnswerAsync(Guid questionId, CreateQuizAnswerRequest request, Guid userId);
    Task<bool> UpdateAnswerAsync(Guid answerId, UpdateQuizAnswerRequest request, Guid userId);
    Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId);

    Task<QuizResultResponse?> SubmitResultAsync(SubmitQuizResultRequest request, Guid userId);
}

public interface IPomodoroService
{
    Task<IEnumerable<PomodoroResponse>> GetAllAsync(Guid userId);
    Task<PomodoroResponse?> GetByIdAsync(Guid id, Guid userId);
    Task<PomodoroResponse?> CreateAsync(CreatePomodoroRequest request, Guid userId);
    Task<bool> UpdateAsync(Guid id, UpdatePomodoroRequest request, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<UserResponse?> GetCurrentUserAsync(ClaimsPrincipal principal);
}
