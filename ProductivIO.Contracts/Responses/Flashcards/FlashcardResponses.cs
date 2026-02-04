namespace ProductivIO.Contracts.Responses.Flashcards;

public record FlashcardResponse(
    Guid Id,
    Guid UserId,
    string Title,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<FlashcardQuestionResponse> Questions
);

public record FlashcardQuestionResponse(
    Guid Id,
    Guid FlashcardId,
    string Question,
    string? Hint,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<FlashcardAnswerResponse> Answers
);

public record FlashcardAnswerResponse(
    Guid Id,
    Guid QuestionId,
    string Answer,
    bool IsCorrect,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
