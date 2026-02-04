using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Contracts.Requests.Notes;

/// <summary>
/// Request contract for updating an existing note.
/// </summary>
public record UpdateNoteRequest(
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
    string Title,
    
    string? Content
);
