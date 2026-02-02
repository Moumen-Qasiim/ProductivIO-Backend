using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Pomodoro
{
    public class CreatePomodoroDto
    {
        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string SessionType { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;
    }
}
