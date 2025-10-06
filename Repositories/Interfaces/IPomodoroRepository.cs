using ProductivIOBackend.DTOs;
using ProductivIOBackend.DTOs.Pomodoro;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Repositories.Interfaces
{
    public interface IPomodoroRepository
    {
        Task<List<PomodoroDto>> GetAllPomodoroAsync(int userId);
        Task<PomodoroDto?> GetPomodoroAsync(int Id, int userId);
        Task<PomodoroDto?> UpdatePomodoroAsync(PomodoroDto pomodoro);
        Task<PomodoroDto?> AddPomodoroAsync(PomodoroDto pomodoro);
        Task<bool> DeletePomodoroAsync(int Id, int userId);
        Task<int> GetCompletedSessionAsync(int userId);
        Task<double> GetTotalDurationAsync(int userId);
    }
}