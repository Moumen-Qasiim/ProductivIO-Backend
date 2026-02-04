using System.ComponentModel.DataAnnotations;
using ProductivIO.Contracts.Enums;
using TaskStatus = ProductivIO.Contracts.Enums.TaskStatus;

namespace ProductivIO.Contracts.Requests.Tasks;

/// <summary>
/// Request contract for updating an existing task.
/// </summary>
public record UpdateTaskRequest(
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
    string Title,
    
    string? Description,
    
    [Required(ErrorMessage = "Priority is required.")]
    TaskPriority Priority,
    
    [Required(ErrorMessage = "Status is required.")]
    TaskStatus Status,
    
    DateTime? DueDate
);
