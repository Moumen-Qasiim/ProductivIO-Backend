using ProductivIO.Contracts.Enums;
using TaskStatus = ProductivIO.Contracts.Enums.TaskStatus;

namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a task with business logic and validation.
/// </summary>
public class Task
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskPriority Priority { get; private set; }
    public TaskStatus Status { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public User? User { get; private set; }

    // Private constructor for EF Core
    private Task() 
    {
        Title = string.Empty;
    }

    /// <summary>
    /// Factory method for creating a new task with validation.
    /// </summary>
    public static Task Create(Guid userId, string title, string? description, TaskPriority priority, TaskStatus status, DateTime? dueDate)
    {
        ValidateTitle(title);
        ValidateUserId(userId);

        return new Task
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Description = description,
            Priority = priority,
            Status = status,
            DueDate = dueDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    /// <summary>
    /// Updates the task with new details.
    /// </summary>
    public void Update(string title, string? description, TaskPriority priority, TaskStatus status, DateTime? dueDate)
    {
        ValidateTitle(title);

        Title = title;
        Description = description;
        Priority = priority;
        Status = status;
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool BelongsTo(Guid userId) => UserId == userId;

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        
        if (title.Length > 255)
            throw new ArgumentException("Title cannot exceed 255 characters.", nameof(title));
    }

    private static void ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
    }
}
