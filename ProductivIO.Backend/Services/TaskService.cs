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

        public async Task<IEnumerable<TaskDto>> GetAll(Guid userId)
        {
            return await _taskRepository.GetAllTasksAsync(userId);
        }

        public async Task<TaskDto?> Get(Guid id, Guid userId)
        {
            return await _taskRepository.GetTaskAsync(id, userId);
        }

        public async Task<TaskDto?> Create(CreateTaskDto task, Guid userId)
        {
            return await _taskRepository.AddTaskAsync(task, userId);
        }

        public async Task<bool> Update(Guid id, UpdateTaskDto task, Guid userId)
        {
            var updated = await _taskRepository.UpdateTaskAsync(id, task, userId);
            return updated != null;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            return await _taskRepository.DeleteTaskAsync(id, userId);
        }
    }
}