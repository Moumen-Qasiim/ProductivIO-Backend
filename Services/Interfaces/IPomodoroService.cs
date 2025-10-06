using ProductivIOBackend.DTOs.Pomodoro;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface IPomodoroService
    {
        Task<IEnumerable<PomodoroDto>> GetAll(int userId);
        Task<PomodoroDto?> Get(int id, int userId);
        Task<PomodoroDto?> Create(PomodoroDto pomodoro);
        Task<bool> Update(int id, PomodoroDto pomodoro);
        Task<bool> Delete(int id, int userId);
        Task<int> GetCompletedSession(int userId);
        Task<double> GetTotalDuration(int userId);
    }
}