
namespace ProductivIO.Backend.DTOs.Quiz
{
    public class QuizzesDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<QuizQuestionDto> Questions { get; set; } = [];
        public IEnumerable<object>? QuizQuestionDto { get; internal set; }
        public IEnumerable<object>? QuizQuestions { get; internal set; }
    }
}