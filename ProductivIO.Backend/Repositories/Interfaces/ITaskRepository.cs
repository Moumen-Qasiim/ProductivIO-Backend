using ProductivIO.Backend.DTOs.Tasks;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskDto>> GetAllTasksAsync(Guid userId);
        Task<TaskDto?> GetTaskAsync(Guid id, Guid userId);
        Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto task, Guid userId);
        Task<TaskDto?> AddTaskAsync(CreateTaskDto task, Guid userId);
        Task<bool> DeleteTaskAsync(Guid id, Guid userId);
    }
}