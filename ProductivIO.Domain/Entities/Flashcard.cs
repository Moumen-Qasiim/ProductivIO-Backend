namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a flashcard set.
/// </summary>
public class Flashcard
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public User? User { get; private set; }
    public ICollection<FlashcardQuestion> Questions { get; private set; } = new List<FlashcardQuestion>();

    // Private constructor for EF Core
    private Flashcard() 
    {
        Title = string.Empty;
    }

    public static Flashcard Create(Guid userId, string title, string? description)
    {
        ValidateTitle(title);
        ValidateUserId(userId);

        return new Flashcard
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Description = description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public void Update(string title, string? description)
    {
        ValidateTitle(title);

        Title = title;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddQuestion(FlashcardQuestion question)
    {
        Questions.Add(question);
        UpdatedAt = DateTime.UtcNow;
    }

    public bool BelongsTo(Guid userId) => UserId == userId;

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
    }

    private static void ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
    }
}
