namespace ProductivIO.Backend.DTOs.Quiz
{
    public class QuizResultDto
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public DateTime TakenAt { get; set; }
        public List<QuizResultAnswerDto> Answers { get; set; } = new();
    }
}