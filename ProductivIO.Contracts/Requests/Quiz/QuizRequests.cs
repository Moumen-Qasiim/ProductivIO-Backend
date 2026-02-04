using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Contracts.Requests.Quiz;

public record CreateQuizRequest(
    [Required] string Title,
    string? Description
);

public record UpdateQuizRequest(
    [Required] string Title,
    string? Description
);

public record CreateQuizQuestionRequest(
    [Required] string Question,
    IEnumerable<CreateQuizAnswerRequest> Answers
);

public record UpdateQuizQuestionRequest(
    [Required] string Question
);

public record CreateQuizAnswerRequest(
    [Required] string Answer,
    bool IsCorrect
);

public record UpdateQuizAnswerRequest(
    [Required] string Answer,
    bool IsCorrect
);

public record SubmitQuizResultRequest(
    [Required] Guid QuizId,
    [Required] IEnumerable<SubmitAnswerRequest> Answers
);

public record SubmitAnswerRequest(
    [Required] Guid QuestionId,
    [Required] Guid AnswerId
);
