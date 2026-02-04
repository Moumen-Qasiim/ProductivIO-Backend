namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing an individual answer within a quiz result.
/// </summary>
public class QuizResultAnswer
{
    public Guid Id { get; private set; }
    public Guid QuizResultId { get; private set; }
    public Guid QuestionId { get; private set; }
    public Guid AnswerId { get; private set; }
    public bool IsCorrect { get; private set; }

    // Navigation properties
    public QuizResult? QuizResult { get; private set; }
    public QuizQuestion? Question { get; private set; }
    public QuizAnswer? Answer { get; private set; }

    private QuizResultAnswer() { }

    public static QuizResultAnswer Create(Guid quizResultId, Guid questionId, Guid answerId, bool isCorrect)
    {
        return new QuizResultAnswer
        {
            Id = Guid.NewGuid(),
            QuizResultId = quizResultId,
            QuestionId = questionId,
            AnswerId = answerId,
            IsCorrect = isCorrect
        };
    }
}
