using ProductivIOBackend.DTOs;
using ProductivIOBackend.DTOs.Pomodoro;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Repositories.Interfaces
{
    public interface IPomodoroRepository
    {
        Task<List<PomodoroDto>> GetAllPomodoroAsync(Guid userId);
        Task<PomodoroDto?> GetPomodoroAsync(Guid id, Guid userId);
        Task<PomodoroDto?> UpdatePomodoroAsync(PomodoroDto pomodoro);
        Task<PomodoroDto?> AddPomodoroAsync(PomodoroDto pomodoro);
        Task<bool> DeletePomodoroAsync(Guid id, Guid userId);
        Task<int> GetCompletedSessionAsync(Guid userId);
        Task<double> GetTotalDurationAsync(Guid userId);
    }
}