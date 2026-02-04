using Microsoft.AspNetCore.Identity;

namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a user in the system.
/// Extends ASP.NET Core Identity User with Guid as the key.
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    

    // Navigation properties
    public ICollection<Note> Notes { get; set; } = [];
    public ICollection<Task> Tasks { get; set; } = [];
    public ICollection<Pomodoro> Pomodoros { get; set; } = [];
    public ICollection<Flashcard> Flashcards { get; set; } = [];
    public ICollection<Quiz> Quizzes { get; set; } = [];
}
