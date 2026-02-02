using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Quiz
{
    public class UpdateQuizAnswerDto
    {
        [Required]
        public string Answer { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;
    }
}
