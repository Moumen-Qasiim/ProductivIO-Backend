namespace ProductivIOBackend.DTOs.Flashcards
{
    public class FlashcardQuestionDto
    {
        public int Id { get; set; }
        public int FlashcardId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string? Hint { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    } 
}