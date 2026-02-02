using ProductivIO.Backend.DTOs.Pomodoro;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IPomodoroService
    {
        Task<IEnumerable<PomodoroDto>> GetAll(Guid userId);
        Task<PomodoroDto?> Get(Guid id, Guid userId);
        Task<PomodoroDto?> Create(CreatePomodoroDto pomodoro, Guid userId);
        Task<bool> Update(Guid id, UpdatePomodoroDto pomodoro, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);
        Task<int> GetCompletedSession(Guid userId);
        Task<double> GetTotalDuration(Guid userId);
    }
}