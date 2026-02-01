using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductivIOBackend.Models;

public class Flashcards
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }

    public ICollection<FlashcardQuestion> FlashcardQuestions { get; set; } = [];
}

public class FlashcardQuestion
{
    [Key]
    public Guid Id { get; set; }

    public Guid FlashcardId { get; set; }

    [ForeignKey("FlashcardId")]
    public Flashcards? Flashcard { get; set; }

    [Required]
    public string Question { get; set; } = string.Empty;

    public string? Hint { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } 

    public DateTime? UpdatedAt { get; set; }

    public ICollection<FlashcardAnswer> Answers { get; set; } = [];
}

public class FlashcardAnswer
{
    [Key]
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    [ForeignKey("QuestionId")]
    public FlashcardQuestion? FlashcardQuestion { get; set; } 

    [Required]
    public string Answer { get; set; } = string.Empty;

    public bool IsCorrect { get; set; } = false;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } 

    public DateTime? UpdatedAt { get; set; }
}
