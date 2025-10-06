
namespace ProductivIOBackend.DTOs.Quiz
{
    public class QuizzesDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<QuizQuestionDto> Questions { get; set; } = new();
        public IEnumerable<object>? QuizQuestionDto { get; internal set; }
        public IEnumerable<object>? QuizQuestions { get; internal set; }
    }
}