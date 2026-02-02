using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Quiz
{
    public class CreateQuizQuestionDto
    {
        [Required]
        public string Question { get; set; } = string.Empty;
    }
}
