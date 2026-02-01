using Microsoft.EntityFrameworkCore;
using ProductivIOBackend.Data;
using ProductivIOBackend.DTOs.Tasks;
using ProductivIOBackend.Models;
using ProductivIOBackend.Repositories.Interfaces;

namespace ProductivIOBackend.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        // Get all tasks for a specific user
        public async Task<List<TaskDto>> GetAllTasksAsync(Guid UserId)
        {
            return await _db.Tasks
                .Where(t => t.UserId == UserId)
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

        // Get a single task by ID
        public async Task<TaskDto?> GetTaskAsync(Guid id, Guid UserId)
        {
            var t = await _db.Tasks
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == UserId);

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

        // Add a new task
        public async Task<TaskDto?> AddTaskAsync(TaskDto dto)
        {
            var user = await _db.Users.FindAsync(dto.UserId);
            if (user == null)
                throw new InvalidOperationException($"User with ID {dto.UserId} not found.");


            var entity = new Tasks
            {
                UserId = dto.UserId,
                User = user,
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = dto.Status,
                DueDate = dto.DueDate,
                CreatedAt = DateTime.Now
            };

            _db.Tasks.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        // Update a task
        public async Task<TaskDto?> UpdateTaskAsync(TaskDto dto)
        {
            var existing = await _db.Tasks
                .FirstOrDefaultAsync(t => t.Id == dto.Id && t.UserId == dto.UserId);

            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Priority = dto.Priority;
            existing.Status = dto.Status;
            existing.DueDate = dto.DueDate;
            existing.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            dto.UpdatedAt = existing.UpdatedAt;
            return dto;
        }

        // Delete a task
        public async Task<bool> DeleteTaskAsync(Guid id, Guid UserId)
        {
            var task = await _db.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == UserId);

            if (task == null) return false;

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
