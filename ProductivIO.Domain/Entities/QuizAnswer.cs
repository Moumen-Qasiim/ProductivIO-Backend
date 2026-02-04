namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing an answer to a quiz question.
/// </summary>
public class QuizAnswer
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public string Answer { get; private set; }
    public bool IsCorrect { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public QuizQuestion? Question { get; private set; }

    private QuizAnswer() 
    {
        Answer = string.Empty;
    }

    public static QuizAnswer Create(Guid questionId, string answer, bool isCorrect)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer text cannot be empty.", nameof(answer));

        return new QuizAnswer
        {
            Id = Guid.NewGuid(),
            QuestionId = questionId,
            Answer = answer,
            IsCorrect = isCorrect,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public void Update(string answer, bool isCorrect)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer text cannot be empty.", nameof(answer));

        Answer = answer;
        IsCorrect = isCorrect;
        UpdatedAt = DateTime.UtcNow;
    }
}
