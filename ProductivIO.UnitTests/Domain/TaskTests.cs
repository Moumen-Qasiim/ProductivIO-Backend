using FluentAssertions;
using ProductivIO.Domain.Entities;
using ProductivIO.Contracts.Enums;
using Xunit;

namespace ProductivIO.UnitTests.Domain;

public class TaskTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnTask()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var title = "Test Task";
        var description = "Test Description";
        var priority = TaskPriority.High;
        var status = ProductivIO.Contracts.Enums.TaskStatus.Todo;
        var dueDate = DateTime.UtcNow.AddDays(1);

        // Act
        var task = ProductivIO.Domain.Entities.Task.Create(userId, title, description, priority, status, dueDate);

        // Assert
        task.Should().NotBeNull();
        task.UserId.Should().Be(userId);
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Priority.Should().Be(priority);
        task.Status.Should().Be(status);
        task.DueDate.Should().Be(dueDate);
    }

    [Fact]
    public void Update_ShouldModifyProperties()
    {
        // Arrange
        var task = ProductivIO.Domain.Entities.Task.Create(Guid.NewGuid(), "Title", "Desc", TaskPriority.Low, ProductivIO.Contracts.Enums.TaskStatus.Todo, null);
        var newTitle = "New Title";
        var newStatus = ProductivIO.Contracts.Enums.TaskStatus.Completed;

        // Act
        task.Update(newTitle, "New Desc", TaskPriority.High, newStatus, null);

        // Assert
        task.Title.Should().Be(newTitle);
        task.Status.Should().Be(newStatus);
        task.UpdatedAt.Should().NotBeNull();
    }
}
