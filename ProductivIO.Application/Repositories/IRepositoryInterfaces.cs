using ProductivIO.Domain.Entities;

namespace ProductivIO.Application.Repositories;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllAsync(Guid userId);
    Task<Note?> GetByIdAsync(Guid id, Guid userId);
    Task<Note> AddAsync(Note note);
    Task<bool> UpdateAsync(Note note);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}

public interface ITaskRepository
{
    Task<IEnumerable<ProductivIO.Domain.Entities.Task>> GetAllAsync(Guid userId);
    Task<ProductivIO.Domain.Entities.Task?> GetByIdAsync(Guid id, Guid userId);
    Task<ProductivIO.Domain.Entities.Task> AddAsync(ProductivIO.Domain.Entities.Task task);
    Task<bool> UpdateAsync(ProductivIO.Domain.Entities.Task task);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}

public interface IFlashcardRepository
{
    Task<IEnumerable<Flashcard>> GetAllAsync(Guid userId);
    Task<Flashcard?> GetByIdAsync(Guid id, Guid userId);
    Task<Flashcard> AddAsync(Flashcard flashcard);
    Task<bool> UpdateAsync(Flashcard flashcard);
    Task<bool> DeleteAsync(Guid id, Guid userId);
    
    Task<FlashcardQuestion?> GetQuestionByIdAsync(Guid id, Guid userId);
    Task<FlashcardQuestion> AddQuestionAsync(FlashcardQuestion question);
    Task<bool> UpdateQuestionAsync(FlashcardQuestion question);
    Task<bool> DeleteQuestionAsync(Guid id, Guid userId);

    Task<FlashcardAnswer?> GetAnswerByIdAsync(Guid id, Guid userId);
    Task<FlashcardAnswer> AddAnswerAsync(FlashcardAnswer answer);
    Task<bool> UpdateAnswerAsync(FlashcardAnswer answer);
    Task<bool> DeleteAnswerAsync(Guid id, Guid userId);
}

public interface IQuizRepository
{
    Task<IEnumerable<Quiz>> GetAllAsync(Guid userId);
    Task<Quiz?> GetByIdAsync(Guid id, Guid userId);
    Task<Quiz> AddAsync(Quiz quiz);
    Task<bool> UpdateAsync(Quiz quiz);
    Task<bool> DeleteAsync(Guid id, Guid userId);

    Task<QuizQuestion?> GetQuestionByIdAsync(Guid id, Guid userId);
    Task<QuizQuestion> AddQuestionAsync(QuizQuestion question);
    Task<bool> UpdateQuestionAsync(QuizQuestion question);
    Task<bool> DeleteQuestionAsync(Guid id, Guid userId);

    Task<QuizAnswer?> GetAnswerByIdAsync(Guid id, Guid userId);
    Task<QuizAnswer> AddAnswerAsync(QuizAnswer answer);
    Task<bool> UpdateAnswerAsync(QuizAnswer answer);
    Task<bool> DeleteAnswerAsync(Guid id, Guid userId);
}

public interface IPomodoroRepository
{
    Task<IEnumerable<Pomodoro>> GetAllAsync(Guid userId);
    Task<Pomodoro?> GetByIdAsync(Guid id, Guid userId);
    Task<Pomodoro> AddAsync(Pomodoro pomodoro);
    Task<bool> UpdateAsync(Pomodoro pomodoro);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}

public interface IQuizResultRepository
{
    Task<IEnumerable<QuizResult>> GetAllAsync(Guid userId);
    Task<QuizResult?> GetByIdAsync(Guid id, Guid userId);
    Task<QuizResult> AddAsync(QuizResult result);
}
