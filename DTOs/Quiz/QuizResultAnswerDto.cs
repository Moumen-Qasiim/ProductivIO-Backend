namespace ProductivIOBackend.DTOs.Quiz
{
    public class QuizResultAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}