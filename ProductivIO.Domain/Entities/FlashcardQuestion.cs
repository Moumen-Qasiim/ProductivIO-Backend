namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a question in a flashcard set.
/// </summary>
public class FlashcardQuestion
{
    public Guid Id { get; private set; }
    public Guid FlashcardId { get; private set; }
    public string Question { get; private set; }
    public string? Hint { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public Flashcard? Flashcard { get; private set; }
    public ICollection<FlashcardAnswer> Answers { get; private set; } = new List<FlashcardAnswer>();

    private FlashcardQuestion() 
    {
        Question = string.Empty;
    }

    public static FlashcardQuestion Create(Guid flashcardId, string question, string? hint)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question cannot be empty.", nameof(question));

        return new FlashcardQuestion
        {
            Id = Guid.NewGuid(),
            FlashcardId = flashcardId,
            Question = question,
            Hint = hint,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public void Update(string question, string? hint)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question cannot be empty.", nameof(question));

        Question = question;
        Hint = hint;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAnswer(FlashcardAnswer answer)
    {
        Answers.Add(answer);
        UpdatedAt = DateTime.UtcNow;
    }
}
