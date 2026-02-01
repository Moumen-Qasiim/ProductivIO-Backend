namespace ProductivIOBackend.DTOs.Flashcards
{
    public class FlashcardQuestionDto
    {
        public Guid Id { get; set; }
        public Guid FlashcardId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string? Hint { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    } 
}