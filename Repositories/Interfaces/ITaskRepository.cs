using ProductivIOBackend.DTOs.Tasks;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskDto>> GetAllTasksAsync(int userId);
        Task<TaskDto?> GetTaskAsync(int Id, int userId);
        Task<TaskDto?> UpdateTaskAsync(TaskDto task);
        Task<TaskDto?> AddTaskAsync(TaskDto task);
        Task<bool> DeleteTaskAsync(int Id, int userId);
    }
}