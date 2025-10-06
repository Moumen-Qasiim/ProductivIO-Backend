using ProductivIOBackend.Models;

namespace ProductivIOBackend.DTOs.Notes
{
    public class NoteDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}