using Microsoft.EntityFrameworkCore;
using ProductivIO.Application.Repositories;
using ProductivIO.Domain.Entities;
using ProductivIO.Infrastructure.Data;

namespace ProductivIO.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;
    public NoteRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Note>> GetAllAsync(Guid userId) =>
        await _context.Notes.Where(n => n.UserId == userId).ToListAsync();

    public async Task<Note?> GetByIdAsync(Guid id, Guid userId) =>
        await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

    public async Task<Note> AddAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<bool> UpdateAsync(Note note)
    {
        _context.Notes.Update(note);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var note = await GetByIdAsync(id, userId);
        if (note == null) return false;
        _context.Notes.Remove(note);
        return await _context.SaveChangesAsync() > 0;
    }
}

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<ProductivIO.Domain.Entities.Task>> GetAllAsync(Guid userId) =>
        await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

    public async Task<ProductivIO.Domain.Entities.Task?> GetByIdAsync(Guid id, Guid userId) =>
        await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    public async Task<ProductivIO.Domain.Entities.Task> AddAsync(ProductivIO.Domain.Entities.Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateAsync(ProductivIO.Domain.Entities.Task task)
    {
        _context.Tasks.Update(task);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var task = await GetByIdAsync(id, userId);
        if (task == null) return false;
        _context.Tasks.Remove(task);
        return await _context.SaveChangesAsync() > 0;
    }
}

public class FlashcardRepository : IFlashcardRepository
{
    private readonly AppDbContext _context;
    public FlashcardRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Flashcard>> GetAllAsync(Guid userId) =>
        await _context.Flashcards.Include(f => f.Questions).ThenInclude(q => q.Answers).Where(f => f.UserId == userId).ToListAsync();

    public async Task<Flashcard?> GetByIdAsync(Guid id, Guid userId) =>
        await _context.Flashcards.Include(f => f.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

    public async Task<Flashcard> AddAsync(Flashcard flashcard)
    {
        _context.Flashcards.Add(flashcard);
        await _context.SaveChangesAsync();
        return flashcard;
    }

    public async Task<bool> UpdateAsync(Flashcard flashcard)
    {
        _context.Flashcards.Update(flashcard);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var flashcard = await GetByIdAsync(id, userId);
        if (flashcard == null) return false;
        _context.Flashcards.Remove(flashcard);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<FlashcardQuestion?> GetQuestionByIdAsync(Guid id, Guid userId) =>
        await _context.FlashcardQuestions.FirstOrDefaultAsync(q => q.Id == id && q.Flashcard != null && q.Flashcard.UserId == userId);

    public async Task<FlashcardQuestion> AddQuestionAsync(FlashcardQuestion question)
    {
        _context.FlashcardQuestions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<bool> UpdateQuestionAsync(FlashcardQuestion question)
    {
        _context.FlashcardQuestions.Update(question);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteQuestionAsync(Guid id, Guid userId)
    {
        var question = await GetQuestionByIdAsync(id, userId);
        if (question == null) return false;
        _context.FlashcardQuestions.Remove(question);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<FlashcardAnswer?> GetAnswerByIdAsync(Guid id, Guid userId) =>
        await _context.FlashcardAnswers.FirstOrDefaultAsync(a => a.Id == id && a.Question != null && a.Question.Flashcard != null && a.Question.Flashcard.UserId == userId);

    public async Task<FlashcardAnswer> AddAnswerAsync(FlashcardAnswer answer)
    {
        _context.FlashcardAnswers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<bool> UpdateAnswerAsync(FlashcardAnswer answer)
    {
        _context.FlashcardAnswers.Update(answer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAnswerAsync(Guid id, Guid userId)
    {
        var answer = await GetAnswerByIdAsync(id, userId);
        if (answer == null) return false;
        _context.FlashcardAnswers.Remove(answer);
        return await _context.SaveChangesAsync() > 0;
    }
}

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;
    public QuizRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Quiz>> GetAllAsync(Guid userId) =>
        await _context.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Answers).Where(q => q.UserId == userId).ToListAsync();

    public async Task<Quiz?> GetByIdAsync(Guid id, Guid userId) =>
        await _context.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

    public async Task<Quiz> AddAsync(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return quiz;
    }

    public async Task<bool> UpdateAsync(Quiz quiz)
    {
        _context.Quizzes.Update(quiz);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var quiz = await GetByIdAsync(id, userId);
        if (quiz == null) return false;
        _context.Quizzes.Remove(quiz);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<QuizQuestion?> GetQuestionByIdAsync(Guid id, Guid userId) =>
        await _context.QuizQuestions.FirstOrDefaultAsync(q => q.Id == id && q.Quiz != null && q.Quiz.UserId == userId);

    public async Task<QuizQuestion> AddQuestionAsync(QuizQuestion question)
    {
        _context.QuizQuestions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<bool> UpdateQuestionAsync(QuizQuestion question)
    {
        _context.QuizQuestions.Update(question);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteQuestionAsync(Guid id, Guid userId)
    {
        var question = await GetQuestionByIdAsync(id, userId);
        if (question == null) return false;
        _context.QuizQuestions.Remove(question);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<QuizAnswer?> GetAnswerByIdAsync(Guid id, Guid userId) =>
        await _context.QuizAnswers.FirstOrDefaultAsync(a => a.Id == id && a.Question != null && a.Question.Quiz != null && a.Question.Quiz.UserId == userId);

    public async Task<QuizAnswer> AddAnswerAsync(QuizAnswer answer)
    {
        _context.QuizAnswers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<bool> UpdateAnswerAsync(QuizAnswer answer)
    {
        _context.QuizAnswers.Update(answer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAnswerAsync(Guid id, Guid userId)
    {
        var answer = await GetAnswerByIdAsync(id, userId);
        if (answer == null) return false;
        _context.QuizAnswers.Remove(answer);
        return await _context.SaveChangesAsync() > 0;
    }
}

public class PomodoroRepository : IPomodoroRepository
{
    private readonly AppDbContext _context;
    public PomodoroRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Pomodoro>> GetAllAsync(Guid userId) =>
        await _context.Pomodoros.Where(s => s.UserId == userId).ToListAsync();

    public async Task<Pomodoro?> GetByIdAsync(Guid id, Guid userId) =>
        await _context.Pomodoros.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

    public async Task<Pomodoro> AddAsync(Pomodoro pomodoro)
    {
        _context.Pomodoros.Add(pomodoro);
        await _context.SaveChangesAsync();
        return pomodoro;
    }

    public async Task<bool> UpdateAsync(Pomodoro pomodoro)
    {
        _context.Pomodoros.Update(pomodoro);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var session = await GetByIdAsync(id, userId);
        if (session == null) return false;
        _context.Pomodoros.Remove(session);
        return await _context.SaveChangesAsync() > 0;
    }
}

public class QuizResultRepository : IQuizResultRepository
{
    private readonly AppDbContext _context;
    public QuizResultRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<QuizResult>> GetAllAsync(Guid userId) =>
        await _context.QuizResults.Include(r => r.ResultAnswers).Where(r => r.UserId == userId).ToListAsync();

    public async Task<QuizResult?> GetByIdAsync(Guid id, Guid userId) =>
        await _context.QuizResults.Include(r => r.ResultAnswers).FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

    public async Task<QuizResult> AddAsync(QuizResult result)
    {
        _context.QuizResults.Add(result);
        await _context.SaveChangesAsync();
        return result;
    }
}
