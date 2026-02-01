namespace ProductivIOBackend.DTOs.Quiz
{
    public class QuizQuestionDto
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string Question { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<QuizAnswerDto> Answers { get; set; } = new();
    }
}