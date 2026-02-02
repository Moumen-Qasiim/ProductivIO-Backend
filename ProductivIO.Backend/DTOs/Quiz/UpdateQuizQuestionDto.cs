using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Quiz
{
    public class UpdateQuizQuestionDto
    {
        [Required]
        public string Question { get; set; } = string.Empty;
    }
}
