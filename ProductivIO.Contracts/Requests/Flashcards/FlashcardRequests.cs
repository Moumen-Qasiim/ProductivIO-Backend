using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Contracts.Requests.Flashcards;

public record CreateFlashcardRequest(
    [Required] string Title,
    string? Description
);

public record UpdateFlashcardRequest(
    [Required] string Title,
    string? Description
);

public record CreateFlashcardQuestionRequest(
    [Required] string Question,
    string? Hint
);

public record UpdateFlashcardQuestionRequest(
    [Required] string Question,
    string? Hint
);

public record CreateFlashcardAnswerRequest(
    [Required] string Answer,
    bool IsCorrect
);

public record UpdateFlashcardAnswerRequest(
    [Required] string Answer,
    bool IsCorrect
);
