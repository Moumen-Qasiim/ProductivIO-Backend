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

        // Get all pomodoro sessions for a specific user
        public async Task<List<PomodoroDto>> GetAllPomodoroAsync(Guid UserId)
        {
            return await _db.Pomodoros
                .Where(p => p.UserId == UserId)
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

        // Get a single pomodoro record
        public async Task<PomodoroDto?> GetPomodoroAsync(Guid id, Guid UserId)
        {
            var pomodoro = await _db.Pomodoros
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == UserId);

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

        // Add a new pomodoro
        public async Task<PomodoroDto?> AddPomodoroAsync(PomodoroDto dto)
        {
            var user = await _db.Users.FindAsync(dto.UserId);
            if (user == null)
                throw new InvalidOperationException($"User with ID {dto.UserId} not found.");

            var entity = new Pomodoro
            {
                UserId = dto.UserId,
                User = user,
                Duration = dto.Duration,
                SessionType = dto.SessionType,
                IsCompleted = dto.IsCompleted,
                CreatedAt = DateTime.Now
            };

            _db.Pomodoros.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        // Update a pomodoro
        public async Task<PomodoroDto?> UpdatePomodoroAsync(PomodoroDto dto)
        {
            var existing = await _db.Pomodoros
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == dto.Id && p.UserId == dto.UserId);

            if (existing == null) return null;

            existing.Duration = dto.Duration;
            existing.SessionType = dto.SessionType;
            existing.IsCompleted = dto.IsCompleted;
            existing.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            dto.UpdatedAt = existing.UpdatedAt;
            return dto;
        }

        // Delete a pomodoro
        public async Task<bool> DeletePomodoroAsync(Guid id, Guid UserId)
        {
            var pomodoro = await _db.Pomodoros
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == UserId);

            if (pomodoro == null) return false;

            _db.Pomodoros.Remove(pomodoro);
            await _db.SaveChangesAsync();
            return true;
        }

        // Get completed work session by id
        public async Task<int> GetCompletedSessionAsync(Guid UserId)
        {
            return await _db.Pomodoros
                .Where(p => p.UserId == UserId && p.SessionType == "work" && p.IsCompleted)
                .CountAsync();
        }

        // Get total work duration 
        public async Task<double> GetTotalDurationAsync(Guid UserId)
        {
            var completedSessions = await _db.Pomodoros
                .Where(s => s.UserId == UserId && s.IsCompleted)
                .ToListAsync();

            double totalDuration = completedSessions.Sum(s => (long?)s.Duration.Ticks) ?? 0;
            return totalDuration;
        }

    }
}
