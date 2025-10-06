namespace ProductivIOBackend.DTOs.Flashcards
{
    public class FlashcardAnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}