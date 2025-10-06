namespace ProductivIOBackend.DTOs.Pomodoro
{
    public class PomodoroDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(0);
        public string SessionType { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}