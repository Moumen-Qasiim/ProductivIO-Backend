namespace ProductivIOBackend.DTOs.Quiz
{
    public class QuizQuestionDto
    {
        public int Id { get; set; }
        public int QuizID { get; set; }
        public string Question { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<QuizAnswerDto> Answers { get; set; } = new();
    }
}