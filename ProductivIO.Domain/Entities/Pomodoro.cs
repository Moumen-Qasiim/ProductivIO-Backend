using ProductivIO.Contracts.Enums;

namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a Pomodoro session.
/// </summary>
public class Pomodoro
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public TimeSpan Duration { get; private set; }
    public SessionType SessionType { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public User? User { get; private set; }

    private Pomodoro() 
    {
    }

    public static Pomodoro Create(Guid userId, TimeSpan duration, SessionType sessionType, bool isCompleted = false)
    {
        ValidateUserId(userId);

        return new Pomodoro
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Duration = duration,
            SessionType = sessionType,
            IsCompleted = isCompleted,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public void Update(TimeSpan duration, SessionType sessionType, bool isCompleted)
    {
        Duration = duration;
        SessionType = sessionType;
        IsCompleted = isCompleted;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        IsCompleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool BelongsTo(Guid userId) => UserId == userId;

    private static void ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
    }
}
