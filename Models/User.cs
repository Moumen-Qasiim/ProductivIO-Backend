using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.Models 
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(265)]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(265)]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 




        public ICollection<Notes> Notes { get; set; } = [];

        public ICollection<Tasks> Tasks { get; set; } = [];

        public ICollection<Pomodoro> Pomodoros { get; set; } = [];

        public ICollection<Flashcards> Flashcards { get; set; } = [];

        public ICollection<Quizzes> Quizzes { get; set; } = [];
    }
}