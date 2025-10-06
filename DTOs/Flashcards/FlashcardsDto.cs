namespace ProductivIOBackend.DTOs.Flashcards
{
    public class FlashcardsDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}