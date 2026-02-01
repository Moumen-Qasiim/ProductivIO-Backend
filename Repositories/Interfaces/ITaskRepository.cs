using ProductivIOBackend.DTOs.Tasks;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskDto>> GetAllTasksAsync(Guid userId);
        Task<TaskDto?> GetTaskAsync(Guid id, Guid userId);
        Task<TaskDto?> UpdateTaskAsync(TaskDto task);
        Task<TaskDto?> AddTaskAsync(TaskDto task);
        Task<bool> DeleteTaskAsync(Guid id, Guid userId);
    }
}