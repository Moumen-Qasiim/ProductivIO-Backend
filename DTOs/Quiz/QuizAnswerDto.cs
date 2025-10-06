namespace ProductivIOBackend.DTOs.Quiz
{
    public class QuizAnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }    
    }
}