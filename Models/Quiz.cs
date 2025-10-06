using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductivIOBackend.Models
{
    public class Quizzes
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    }

    public class QuizQuestion
    {
        [Key]
        public int Id { get; set; }

        public int QuizID { get; set; }

        [ForeignKey("QuizID")]
        public Quizzes? Quiz { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        public string Question { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<QuizAnswer> Answers { get; set; } = new List<QuizAnswer>();
    }

    public class QuizAnswer
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public QuizQuestion? QuizQuestion { get; set; }

        [Required(ErrorMessage = "Answer is required.")]
        public string Answer { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }

}