namespace ProductivIO.Contracts.Responses.Quiz;

public record QuizResponse(
    Guid Id,
    Guid UserId,
    string Title,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<QuizQuestionResponse> Questions
);

public record QuizQuestionResponse(
    Guid Id,
    Guid QuizId,
    string Question,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<QuizAnswerResponse> Answers
);

public record QuizAnswerResponse(
    Guid Id,
    Guid QuestionId,
    string Answer,
    bool IsCorrect,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuizResultResponse(
    Guid Id,
    Guid UserId,
    Guid QuizId,
    int Score,
    int TotalQuestions,
    int CorrectAnswers,
    DateTime CreatedAt,
    IEnumerable<QuizResultAnswerResponse> Answers
);

public record QuizResultAnswerResponse(
    Guid Id,
    Guid QuestionId,
    Guid AnswerId,
    bool IsCorrect
);
