using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.DTOs.Pomodoro;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Repositories.Interfaces;

namespace ProductivIO.Backend.Repositories
{
    public class PomodoroRepository : IPomodoroRepository
    {
        private readonly AppDbContext _db;

        public PomodoroRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PomodoroDto>> GetAllPomodoroAsync(Guid userId)
        {
            return await _db.Pomodoros
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PomodoroDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Duration = p.Duration,
                    SessionType = p.SessionType,
                    IsCompleted = p.IsCompleted,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<PomodoroDto?> GetPomodoroAsync(Guid id, Guid userId)
        {
            var p = await _db.Pomodoros
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (p == null) return null;

            return new PomodoroDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Duration = p.Duration,
                SessionType = p.SessionType,
                IsCompleted = p.IsCompleted,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };
        }

        public async Task<PomodoroDto?> AddPomodoroAsync(CreatePomodoroDto dto, Guid userId)
        {
            var entity = new Pomodoro
            {
                UserId = userId,
                Duration = dto.Duration,
                SessionType = dto.SessionType,
                IsCompleted = dto.IsCompleted
            };

            _db.Pomodoros.Add(entity);
            await _db.SaveChangesAsync();

            return new PomodoroDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Duration = entity.Duration,
                SessionType = entity.SessionType,
                IsCompleted = entity.IsCompleted,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<PomodoroDto?> UpdatePomodoroAsync(Guid id, UpdatePomodoroDto dto, Guid userId)
        {
            var existing = await _db.Pomodoros
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (existing == null) return null;

            existing.Duration = dto.Duration;
            existing.SessionType = dto.SessionType;
            existing.IsCompleted = dto.IsCompleted;

            await _db.SaveChangesAsync();

            return new PomodoroDto
            {
                Id = existing.Id,
                UserId = existing.UserId,
                Duration = existing.Duration,
                SessionType = existing.SessionType,
                IsCompleted = existing.IsCompleted,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeletePomodoroAsync(Guid id, Guid userId)
        {
            var pomodoro = await _db.Pomodoros
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (pomodoro == null) return false;

            _db.Pomodoros.Remove(pomodoro);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetCompletedSessionAsync(Guid userId)
        {
            return await _db.Pomodoros
                .Where(p => p.UserId == userId && p.SessionType == "work" && p.IsCompleted)
                .CountAsync();
        }

        public async Task<double> GetTotalDurationAsync(Guid userId)
        {
            var completedSessions = await _db.Pomodoros
                .Where(s => s.UserId == userId && s.IsCompleted)
                .ToListAsync();

            return completedSessions.Sum(s => s.Duration.TotalSeconds);
        }
    }
}
