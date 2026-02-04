using Moq;
using FluentAssertions;
using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services;
using ProductivIO.Contracts.Requests.Tasks;
using ProductivIO.Contracts.Enums;
using TaskStatus = ProductivIO.Contracts.Enums.TaskStatus;

namespace ProductivIO.UnitTests.Services;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _taskService = new TaskService(_taskRepositoryMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateAsync_ShouldReturnTaskResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateTaskRequest("Title", "Desc", TaskPriority.High, TaskStatus.Todo, null);
        _taskRepositoryMock.Setup(x => x.AddAsync(It.IsAny<ProductivIO.Domain.Entities.Task>()))
            .ReturnsAsync((ProductivIO.Domain.Entities.Task t) => t);

        // Act
        var result = await _taskService.CreateAsync(request, userId);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("Title");
        result.UserId.Should().Be(userId);
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateAsync_ShouldReturnTrue_WhenTaskExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var task = ProductivIO.Domain.Entities.Task.Create(userId, "Old", "Old", TaskPriority.Low, TaskStatus.Todo, null);
        var request = new UpdateTaskRequest("New", "New", TaskPriority.High, TaskStatus.Completed, null);

        _taskRepositoryMock.Setup(x => x.GetByIdAsync(taskId, userId)).ReturnsAsync(task);
        _taskRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<ProductivIO.Domain.Entities.Task>())).ReturnsAsync(true);

        // Act
        var result = await _taskService.UpdateAsync(taskId, request, userId);

        // Assert
        result.Should().BeTrue();
        task.Title.Should().Be("New");
    }
}
