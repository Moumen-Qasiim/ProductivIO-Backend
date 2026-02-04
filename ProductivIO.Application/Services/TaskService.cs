using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Application.Mapping;
using ProductivIO.Contracts.Requests.Tasks;
using ProductivIO.Contracts.Responses.Tasks;

namespace ProductivIO.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskResponse>> GetAllAsync(Guid userId)
    {
        var tasks = await _taskRepository.GetAllAsync(userId);
        return tasks.Select(t => t.ToResponse());
    }

    public async Task<TaskResponse?> GetByIdAsync(Guid id, Guid userId)
    {
        var task = await _taskRepository.GetByIdAsync(id, userId);
        return task?.ToResponse();
    }

    public async Task<TaskResponse?> CreateAsync(CreateTaskRequest request, Guid userId)
    {
        var task = ProductivIO.Domain.Entities.Task.Create(
            userId, request.Title, request.Description, request.Priority, request.Status, request.DueDate);
        var created = await _taskRepository.AddAsync(task);
        return created.ToResponse();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateTaskRequest request, Guid userId)
    {
        var task = await _taskRepository.GetByIdAsync(id, userId);
        if (task == null) return false;

        task.Update(request.Title, request.Description, request.Priority, request.Status, request.DueDate);
        return await _taskRepository.UpdateAsync(task);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        return await _taskRepository.DeleteAsync(id, userId);
    }
}
