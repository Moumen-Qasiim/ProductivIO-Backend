using System.ComponentModel.DataAnnotations;
using ProductivIO.Contracts.Enums;

namespace ProductivIO.Contracts.Requests.Pomodoro;

public record CreatePomodoroRequest(
    [Required] TimeSpan Duration,
    [Required] SessionType SessionType,
    bool IsCompleted = false
);

public record UpdatePomodoroRequest(
    [Required] TimeSpan Duration,
    [Required] SessionType SessionType,
    bool IsCompleted
);
