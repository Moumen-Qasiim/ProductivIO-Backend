using ProductivIO.Backend.DTOs.Tasks;
using ProductivIO.Backend.Repositories.Interfaces;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<IEnumerable<TaskDto>> GetAll(Guid UserId)
        {
            var tasks = await _taskRepository.GetAllTasksAsync(UserId);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                DueDate = t.DueDate,
                UserId = t.UserId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            });
        }

        public async Task<TaskDto?> Get(Guid id, Guid UserId)
        {
            var task = await _taskRepository.GetTaskAsync(id, UserId);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                DueDate = task.DueDate,
                UserId = task.UserId,
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
                UserId = created.UserId,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> Update(Guid id, TaskDto task)
        {
            if (id != task.Id) return false;

            var updated = await _taskRepository.UpdateTaskAsync(task);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> Delete(Guid id, Guid UserId)
        {
            return await _taskRepository.DeleteTaskAsync(id, UserId);
        }
    }
}