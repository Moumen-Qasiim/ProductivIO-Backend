using ProductivIO.Contracts.Responses.Notes;
using ProductivIO.Domain.Entities;
using ProductivIO.Contracts.Responses.Tasks;
using ProductivIO.Contracts.Responses.Flashcards;
using ProductivIO.Contracts.Responses.Quiz;
using ProductivIO.Contracts.Responses.Pomodoro;
using ProductivIO.Contracts.Responses.Auth;

namespace ProductivIO.Application.Mapping;

public static class EntityMappers
{
    public static NoteResponse ToResponse(this Note note) =>
        new NoteResponse(note.Id, note.UserId, note.Title, note.Content, note.CreatedAt, note.UpdatedAt);

    public static TaskResponse ToResponse(this ProductivIO.Domain.Entities.Task task) =>
        new TaskResponse(task.Id, task.UserId, task.Title, task.Description, task.Priority, task.Status, task.DueDate, task.CreatedAt, task.UpdatedAt);

    public static FlashcardResponse ToResponse(this Flashcard flashcard) =>
        new FlashcardResponse(
            flashcard.Id, 
            flashcard.UserId, 
            flashcard.Title, 
            flashcard.Description, 
            flashcard.CreatedAt, 
            flashcard.UpdatedAt,
            flashcard.Questions?.Select(q => q.ToResponse()) ?? []
        );

    public static FlashcardQuestionResponse ToResponse(this FlashcardQuestion question) =>
        new FlashcardQuestionResponse(
            question.Id,
            question.FlashcardId,
            question.Question,
            question.Hint,
            question.CreatedAt,
            question.UpdatedAt,
            question.Answers?.Select(a => a.ToResponse()) ?? []
        );

    public static FlashcardAnswerResponse ToResponse(this FlashcardAnswer answer) =>
        new FlashcardAnswerResponse(answer.Id, answer.QuestionId, answer.Answer, answer.IsCorrect, answer.CreatedAt, answer.UpdatedAt);

    public static QuizResponse ToResponse(this Quiz quiz) =>
        new QuizResponse(
            quiz.Id,
            quiz.UserId,
            quiz.Title,
            quiz.Description,
            quiz.CreatedAt,
            quiz.UpdatedAt,
            quiz.Questions?.Select(q => q.ToResponse()) ?? []
        );

    public static QuizQuestionResponse ToResponse(this QuizQuestion question) =>
        new QuizQuestionResponse(
            question.Id,
            question.QuizId,
            question.Question,
            question.CreatedAt,
            question.UpdatedAt,
            question.Answers?.Select(a => a.ToResponse()) ?? []
        );

    public static QuizAnswerResponse ToResponse(this QuizAnswer answer) =>
        new QuizAnswerResponse(answer.Id, answer.QuestionId, answer.Answer, answer.IsCorrect, answer.CreatedAt, answer.UpdatedAt);

    public static PomodoroResponse ToResponse(this Pomodoro pomodoro) =>
        new PomodoroResponse(pomodoro.Id, pomodoro.UserId, pomodoro.Duration, pomodoro.SessionType, pomodoro.IsCompleted, pomodoro.CreatedAt, pomodoro.UpdatedAt);

    public static QuizResultResponse ToResponse(this QuizResult result) =>
        new QuizResultResponse(
            result.Id,
            result.UserId,
            result.QuizId,
            result.Score,
            result.TotalQuestions,
            result.CorrectAnswers,
            result.CreatedAt,
            result.ResultAnswers?.Select(a => a.ToResponse()) ?? []
        );

    public static QuizResultAnswerResponse ToResponse(this QuizResultAnswer answer) =>
        new QuizResultAnswerResponse(answer.Id, answer.QuestionId, answer.AnswerId, answer.IsCorrect);

    public static UserResponse ToResponse(this User user) =>
        new UserResponse(user.Id, user.FirstName, user.LastName, user.Email ?? string.Empty, user.CreatedAt);
}
