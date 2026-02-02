using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Flashcards
{
    public class CreateFlashcardQuestionDto
    {
        [Required]
        public string Question { get; set; } = string.Empty;

        public string? Hint { get; set; }
    }
}
