namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing an answer to a flashcard question.
/// </summary>
public class FlashcardAnswer
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public string Answer { get; private set; }
    public bool IsCorrect { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public FlashcardQuestion? Question { get; private set; }

    private FlashcardAnswer() 
    {
        Answer = string.Empty;
    }

    public static FlashcardAnswer Create(Guid questionId, string answer, bool isCorrect)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer cannot be empty.", nameof(answer));

        return new FlashcardAnswer
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
            throw new ArgumentException("Answer cannot be empty.", nameof(answer));

        Answer = answer;
        IsCorrect = isCorrect;
        UpdatedAt = DateTime.UtcNow;
    }
}
