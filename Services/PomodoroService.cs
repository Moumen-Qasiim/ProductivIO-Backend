using System.Threading.Tasks;
using ProductivIOBackend.Models;
using ProductivIOBackend.DTOs.Pomodoro;
using ProductivIOBackend.Repositories.Interfaces;
using ProductivIOBackend.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;


namespace ProductivIOBackend.Services
{
    public class PomodoroService : IPomodoroService
    {
        private readonly IPomodoroRepository _pomodoroRepository;

        public PomodoroService(IPomodoroRepository pomodoroRepository)
        {
            _pomodoroRepository = pomodoroRepository;
        }

        public async Task<IEnumerable<PomodoroDto>> GetAll(int userId)
        {
            var pomodoros = await _pomodoroRepository.GetAllPomodoroAsync(userId);

            return pomodoros.Select(p => new PomodoroDto
            {
                Id = p.Id,
                UserID = p.UserID,
                Duration = p.Duration,
                SessionType = p.SessionType,
                IsCompleted = p.IsCompleted,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }

        public async Task<PomodoroDto?> Get(int id, int userId)
        {
            var pomodoro = await _pomodoroRepository.GetPomodoroAsync(id, userId);
            if (pomodoro == null) return null;

            return new PomodoroDto
            {
                Id = pomodoro.Id,
                UserID = pomodoro.UserID,
                Duration = pomodoro.Duration,
                SessionType = pomodoro.SessionType,
                IsCompleted = pomodoro.IsCompleted,
                CreatedAt = pomodoro.CreatedAt,
                UpdatedAt = pomodoro.UpdatedAt
            };
        }

        public async Task<PomodoroDto?> Create(PomodoroDto pomodoro)
        {
            var created = await _pomodoroRepository.AddPomodoroAsync(pomodoro);
            if (created == null) return null;

            return new PomodoroDto
            {
                Id = created.Id,
                UserID = created.UserID,
                Duration = created.Duration,
                SessionType = created.SessionType,
                IsCompleted = created.IsCompleted,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> Update(int id, PomodoroDto pomodoro)
        {
            if (id != pomodoro.Id) return false;

            var updated = await _pomodoroRepository.UpdatePomodoroAsync(pomodoro);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            return await _pomodoroRepository.DeletePomodoroAsync(id, userId);
        }

        public async Task<int> GetCompletedSession(int userId)
        {
            return await _pomodoroRepository.GetCompletedSessionAsync(userId);
        }

        public async Task<double> GetTotalDuration(int userId)
        {
            return await _pomodoroRepository.GetTotalDurationAsync(userId);
        }
    }
}