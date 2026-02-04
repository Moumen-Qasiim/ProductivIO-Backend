
namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a result of a quiz attempt.
/// </summary>
public class QuizResult
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid QuizId { get; private set; }
    public int Score { get; private set; }
    public int TotalQuestions { get; private set; }
    public int CorrectAnswers { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    public User? User { get; private set; }
    public Quiz? Quiz { get; private set; }
    public ICollection<QuizResultAnswer> ResultAnswers { get; private set; } = new List<QuizResultAnswer>();

    private QuizResult() { }

    public static QuizResult Create(Guid userId, Guid quizId, int score, int totalQuestions, int correctAnswers)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        if (quizId == Guid.Empty) throw new ArgumentException("Quiz ID cannot be empty.", nameof(quizId));

        return new QuizResult
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            QuizId = quizId,
            Score = score,
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddResultAnswer(QuizResultAnswer resultAnswer)
    {
        ResultAnswers.Add(resultAnswer);
    }
}
