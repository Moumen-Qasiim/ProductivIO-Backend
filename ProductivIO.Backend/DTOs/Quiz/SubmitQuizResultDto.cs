using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Quiz
{
    public class SubmitQuizResultDto
    {
        [Required]
        public Guid QuizId { get; set; }

        [Required]
        public List<QuizResultAnswerDto> Answers { get; set; } = new();
    }
}
