using ProductivIO.Backend.DTOs.Pomodoro;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IPomodoroService
    {
        Task<IEnumerable<PomodoroDto>> GetAll(Guid userId);
        Task<PomodoroDto?> Get(Guid id, Guid userId);
        Task<PomodoroDto?> Create(PomodoroDto pomodoro);
        Task<bool> Update(Guid id, PomodoroDto pomodoro);
        Task<bool> Delete(Guid id, Guid userId);
        Task<int> GetCompletedSession(Guid userId);
        Task<double> GetTotalDuration(Guid userId);
    }
}