using ProductivIO.Contracts.Enums;
using TaskStatus = ProductivIO.Contracts.Enums.TaskStatus;

namespace ProductivIO.Contracts.Responses.Tasks;

/// <summary>
/// Response contract representing a task.
/// </summary>
public record TaskResponse(
    Guid Id,
    Guid UserId,
    string Title,
    string? Description,
    TaskPriority Priority,
    ProductivIO.Contracts.Enums.TaskStatus Status,
    DateTime? DueDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
