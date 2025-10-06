using ProductivIOBackend.Models;

namespace ProductivIOBackend.Models
{
    public class QuizResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid QuizId { get; set; }

        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }

        public DateTime TakenAt { get; set; } 

        
        public User User { get; set; } = null!;
        public Quizzes Quiz { get; set; } = null!;
        public List<QuizResultAnswer> ResultAnswers { get; set; } = [];
    }

    public class QuizResultAnswer
    {
        public Guid Id { get; set; }
        public Guid QuizResultId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }

        public bool IsCorrect { get; set; }

        public QuizResult QuizResult { get; set; } = null!;
        public QuizQuestion Question { get; set; } = null!;
        public QuizAnswer Answer { get; set; } = null!;
    }
}