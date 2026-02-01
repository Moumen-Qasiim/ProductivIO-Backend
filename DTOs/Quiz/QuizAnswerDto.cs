namespace ProductivIO.Backend.DTOs.Quiz
{
    public class QuizAnswerDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }    
    }
}