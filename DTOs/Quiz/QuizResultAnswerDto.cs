namespace ProductivIOBackend.DTOs.Quiz
{
    public class QuizResultAnswerDto
    {
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}