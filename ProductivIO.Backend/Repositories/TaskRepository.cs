using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.DTOs.Tasks;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Repositories.Interfaces;

namespace ProductivIO.Backend.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TaskDto>> GetAllTasksAsync(Guid userId)
        {
            return await _db.Tasks
                .Where(t => t.UserId == userId)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                })
                .OrderByDescending(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<TaskDto?> GetTaskAsync(Guid id, Guid userId)
        {
            var t = await _db.Tasks
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (t == null) return null;

            return new TaskDto
            {
                Id = t.Id,
                UserId = t.UserId,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            };
        }

        public async Task<TaskDto?> AddTaskAsync(CreateTaskDto dto, Guid userId)
        {
            var entity = new Tasks
            {
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = dto.Status,
                DueDate = dto.DueDate
            };

            _db.Tasks.Add(entity);
            await _db.SaveChangesAsync();

            return new TaskDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Title = entity.Title,
                Description = entity.Description,
                Priority = entity.Priority,
                Status = entity.Status,
                DueDate = entity.DueDate,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto dto, Guid userId)
        {
            var existing = await _db.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Priority = dto.Priority;
            existing.Status = dto.Status;
            existing.DueDate = dto.DueDate;

            await _db.SaveChangesAsync();

            return new TaskDto
            {
                Id = existing.Id,
                UserId = existing.UserId,
                Title = existing.Title,
                Description = existing.Description,
                Priority = existing.Priority,
                Status = existing.Status,
                DueDate = existing.DueDate,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteTaskAsync(Guid id, Guid userId)
        {
            var task = await _db.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null) return false;

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
