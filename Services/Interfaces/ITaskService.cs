using System.Diagnostics;
using ProductivIOBackend.DTOs.Tasks;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAll(int userId);
        Task<TaskDto?> Get(int id, int userId);
        Task<TaskDto?> Create(TaskDto task);
        Task<bool> Update(int id, TaskDto task);
        Task<bool> Delete(int id, int userId);
    }
}