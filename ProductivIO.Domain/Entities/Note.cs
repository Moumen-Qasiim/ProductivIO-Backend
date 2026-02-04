namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a note with business logic and validation.
/// </summary>
public class Note
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public User? User { get; private set; }

    // Private constructor for EF Core
    private Note() 
    {
        Title = string.Empty;
    }

    /// <summary>
    /// Factory method for creating a new note with validation.
    /// </summary>
    /// <param name="userId">The ID of the user creating the note.</param>
    /// <param name="title">The title of the note.</param>
    /// <param name="content">The content of the note.</param>
    /// <returns>A new Note instance.</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    public static Note Create(Guid userId, string title, string? content)
    {
        ValidateTitle(title);
        ValidateUserId(userId);

        return new Note
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Content = content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    /// <summary>
    /// Updates the note with new title and content.
    /// </summary>
    /// <param name="title">The new title.</param>
    /// <param name="content">The new content.</param>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    public void Update(string title, string? content)
    {
        ValidateTitle(title);

        Title = title;
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the note belongs to the specified user.
    /// </summary>
    /// <param name="userId">The user ID to check.</param>
    /// <returns>True if the note belongs to the user; otherwise, false.</returns>
    public bool BelongsTo(Guid userId) => UserId == userId;

    // Business rule: Title validation
    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (title.Length > 255)
            throw new ArgumentException("Title cannot exceed 255 characters.", nameof(title));
    }

    // Business rule: UserId validation
    private static void ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
    }
}
