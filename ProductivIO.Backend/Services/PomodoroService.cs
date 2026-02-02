using ProductivIO.Backend.DTOs.Pomodoro;
using ProductivIO.Backend.Repositories.Interfaces;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class PomodoroService : IPomodoroService
    {
        private readonly IPomodoroRepository _pomodoroRepository;

        public PomodoroService(IPomodoroRepository pomodoroRepository)
        {
            _pomodoroRepository = pomodoroRepository;
        }

        public async Task<IEnumerable<PomodoroDto>> GetAll(Guid userId)
        {
            return await _pomodoroRepository.GetAllPomodoroAsync(userId);
        }

        public async Task<PomodoroDto?> Get(Guid id, Guid userId)
        {
            return await _pomodoroRepository.GetPomodoroAsync(id, userId);
        }

        public async Task<PomodoroDto?> Create(CreatePomodoroDto pomodoro, Guid userId)
        {
            return await _pomodoroRepository.AddPomodoroAsync(pomodoro, userId);
        }

        public async Task<bool> Update(Guid id, UpdatePomodoroDto pomodoro, Guid userId)
        {
            var updated = await _pomodoroRepository.UpdatePomodoroAsync(id, pomodoro, userId);
            return updated != null;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            return await _pomodoroRepository.DeletePomodoroAsync(id, userId);
        }

        public async Task<int> GetCompletedSession(Guid userId)
        {
            return await _pomodoroRepository.GetCompletedSessionAsync(userId);
        }

        public async Task<double> GetTotalDuration(Guid userId)
        {
            return await _pomodoroRepository.GetTotalDurationAsync(userId);
        }
    }
}