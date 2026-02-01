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

        public async Task<IEnumerable<PomodoroDto>> GetAll(Guid   UserId)
        {
            var pomodoros = await _pomodoroRepository.GetAllPomodoroAsync(UserId);

            return pomodoros.Select(p => new PomodoroDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Duration = p.Duration,
                SessionType = p.SessionType,
                IsCompleted = p.IsCompleted,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }

        public async Task<PomodoroDto?> Get(Guid   id, Guid   UserId)
        {
            var pomodoro = await _pomodoroRepository.GetPomodoroAsync(id, UserId);
            if (pomodoro == null) return null;

            return new PomodoroDto
            {
                Id = pomodoro.Id,
                UserId = pomodoro.UserId,
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
                UserId = created.UserId,
                Duration = created.Duration,
                SessionType = created.SessionType,
                IsCompleted = created.IsCompleted,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> Update(Guid   id, PomodoroDto pomodoro)
        {
            if (id != pomodoro.Id) return false;

            var updated = await _pomodoroRepository.UpdatePomodoroAsync(pomodoro);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> Delete(Guid   id, Guid   UserId)
        {
            return await _pomodoroRepository.DeletePomodoroAsync(id, UserId);
        }

        public async Task<int> GetCompletedSession(Guid   UserId)
        {
            return await _pomodoroRepository.GetCompletedSessionAsync(UserId);
        }

        public async Task<double> GetTotalDuration(Guid   UserId)
        {
            return await _pomodoroRepository.GetTotalDurationAsync(UserId);
        }
    }
}