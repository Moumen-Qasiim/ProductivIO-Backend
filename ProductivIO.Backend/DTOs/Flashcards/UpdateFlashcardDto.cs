using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Flashcards
{
    public class UpdateFlashcardDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
