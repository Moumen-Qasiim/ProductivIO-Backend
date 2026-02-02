using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Flashcards
{
    public class CreateFlashcardAnswerDto
    {
        [Required]
        public string Answer { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;
    }
}
