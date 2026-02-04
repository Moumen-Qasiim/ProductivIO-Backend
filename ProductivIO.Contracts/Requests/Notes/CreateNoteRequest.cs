using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Contracts.Requests.Notes;

/// <summary>
/// Request contract for creating a new note.
/// </summary>
public record CreateNoteRequest(
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
    string Title,
    
    string? Content
);
