using System.Threading.Tasks;
using ProductivIOBackend.DTOs;
using ProductivIOBackend.Services.Interfaces;
using ProductivIOBackend.Repositories.Interfaces;
using ProductivIOBackend.DTOs.Tasks;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<IEnumerable<TaskDto>> GetAll(int userId)
        {
            var tasks = await _taskRepository.GetAllTasksAsync(userId);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                DueDate = t.DueDate,
                UserID = t.UserID,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            });
        }

        public async Task<TaskDto?> Get(int id, int userId)
        {
            var task = await _taskRepository.GetTaskAsync(id, userId);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                DueDate = task.DueDate,
                UserID = task.UserID,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        public async Task<TaskDto?> Create(TaskDto task)
        {
            var created = await _taskRepository.AddTaskAsync(task);
            if (created == null) return null;

            return new TaskDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                Priority = created.Priority,
                Status = created.Status,
                DueDate = created.DueDate,
                UserID = created.UserID,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> Update(int id, TaskDto task)
        {
            if (id != task.Id) return false;

            var updated = await _taskRepository.UpdateTaskAsync(task);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            return await _taskRepository.DeleteTaskAsync(id, userId);
        }
    }
}