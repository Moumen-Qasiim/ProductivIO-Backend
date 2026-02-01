using ProductivIO.Backend.DTOs.Tasks;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAll(Guid userId);
        Task<TaskDto?> Get(Guid id, Guid   userId);
        Task<TaskDto?> Create(TaskDto task);
        Task<bool> Update(Guid id, TaskDto task);
        Task<bool> Delete(Guid id, Guid   userId);
    }
}