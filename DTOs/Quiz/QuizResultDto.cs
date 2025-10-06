namespace ProductivIOBackend.DTOs.Quiz
{
    public class QuizResultDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public DateTime TakenAt { get; set; }
        public List<QuizResultAnswerDto> Answers { get; set; } = new();
    }
}