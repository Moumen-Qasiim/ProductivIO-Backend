namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a question in a quiz.
/// </summary>
public class QuizQuestion
{
    public Guid Id { get; private set; }
    public Guid QuizId { get; private set; }
    public string Question { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public Quiz? Quiz { get; private set; }
    public ICollection<QuizAnswer> Answers { get; private set; } = new List<QuizAnswer>();

    private QuizQuestion() 
    {
        Question = string.Empty;
    }

    public static QuizQuestion Create(Guid quizId, string question)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question text cannot be empty.", nameof(question));

        return new QuizQuestion
        {
            Id = Guid.NewGuid(),
            QuizId = quizId,
            Question = question,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public void Update(string question)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question text cannot be empty.", nameof(question));

        Question = question;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAnswer(QuizAnswer answer)
    {
        Answers.Add(answer);
    }
}
