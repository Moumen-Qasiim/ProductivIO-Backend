namespace ProductivIO.Contracts.Responses.Notes;

/// <summary>
/// Response contract representing a note.
/// </summary>
public record NoteResponse(
    Guid Id,
    Guid UserId,
    string Title,
    string? Content,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
