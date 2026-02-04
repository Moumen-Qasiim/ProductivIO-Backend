using ProductivIO.Contracts.Enums;

namespace ProductivIO.Contracts.Responses.Pomodoro;

public record PomodoroResponse(
    Guid Id,
    Guid UserId,
    TimeSpan Duration,
    SessionType SessionType,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
