using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Application.Mapping;
using ProductivIO.Contracts.Requests.Pomodoro;
using ProductivIO.Contracts.Responses.Pomodoro;
using ProductivIO.Domain.Entities;

namespace ProductivIO.Application.Services;

public class PomodoroService : IPomodoroService
{
    private readonly IPomodoroRepository _pomodoroRepository;

    public PomodoroService(IPomodoroRepository pomodoroRepository)
    {
        _pomodoroRepository = pomodoroRepository;
    }

    public async Task<IEnumerable<PomodoroResponse>> GetAllAsync(Guid userId)
    {
        var sessions = await _pomodoroRepository.GetAllAsync(userId);
        return sessions.Select(s => s.ToResponse());
    }

    public async Task<PomodoroResponse?> GetByIdAsync(Guid id, Guid userId)
    {
        var session = await _pomodoroRepository.GetByIdAsync(id, userId);
        return session?.ToResponse();
    }

    public async Task<PomodoroResponse?> CreateAsync(CreatePomodoroRequest request, Guid userId)
    {
        var session = Pomodoro.Create(userId, request.Duration, request.SessionType, request.IsCompleted);
        var created = await _pomodoroRepository.AddAsync(session);
        return created.ToResponse();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePomodoroRequest request, Guid userId)
    {
        var session = await _pomodoroRepository.GetByIdAsync(id, userId);
        if (session == null) return false;

        session.Update(request.Duration, request.SessionType, request.IsCompleted);
        return await _pomodoroRepository.UpdateAsync(session);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        return await _pomodoroRepository.DeleteAsync(id, userId);
    }
}
