using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Quiz
{
    public class CreateQuizDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
