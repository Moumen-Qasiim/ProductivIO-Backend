namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a quiz.
/// </summary>
public class Quiz
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public User? User { get; private set; }
    public ICollection<QuizQuestion> Questions { get; private set; } = new List<QuizQuestion>();

    private Quiz() 
    {
        Title = string.Empty;
    }

    public static Quiz Create(Guid userId, string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        return new Quiz
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
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Title = title;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddQuestion(QuizQuestion question)
    {
        Questions.Add(question);
        UpdatedAt = DateTime.UtcNow;
    }

    public bool BelongsTo(Guid userId) => UserId == userId;
}
