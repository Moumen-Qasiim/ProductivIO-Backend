using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductivIOBackend.Models;

public class Flashcards
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserID { get; set; }

    [ForeignKey("UserID")]
    public User? User { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<FlashcardQuestion> FlashcardQuestions { get; set; } = new List<FlashcardQuestion>();
}

public class FlashcardQuestion
{
    [Key]
    public int Id { get; set; }

    public int FlashcardId { get; set; }

    [ForeignKey("FlashcardId")]
    public Flashcards? Flashcard { get; set; }

    [Required]
    public string Question { get; set; } = string.Empty;

    public string? Hint { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<FlashcardAnswer> Answers { get; set; } = new List<FlashcardAnswer>();
}

public class FlashcardAnswer
{
    [Key]
    public int Id { get; set; }

    public int QuestionId { get; set; }

    [ForeignKey("QuestionId")]
    public FlashcardQuestion? FlashcardQuestion { get; set; } 

    [Required]
    public string Answer { get; set; } = string.Empty;

    public bool IsCorrect { get; set; } = false;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}
