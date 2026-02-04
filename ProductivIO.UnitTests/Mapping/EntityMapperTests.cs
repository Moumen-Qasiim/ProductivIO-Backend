using FluentAssertions;
using ProductivIO.Application.Mapping;
using ProductivIO.Domain.Entities;
using ProductivIO.Contracts.Enums;
using TaskStatus = ProductivIO.Contracts.Enums.TaskStatus;
using Xunit;

namespace ProductivIO.UnitTests.Mapping;

public class EntityMapperTests
{
    [Fact]
    public void Task_ToResponse_ShouldMapCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var task = ProductivIO.Domain.Entities.Task.Create(userId, "Title", "Desc", TaskPriority.High, TaskStatus.Todo, DateTime.UtcNow);

        // Act
        var response = task.ToResponse();

        // Assert
        response.Id.Should().Be(task.Id);
        response.UserId.Should().Be(task.UserId);
        response.Title.Should().Be(task.Title);
        response.Description.Should().Be(task.Description);
        response.Priority.Should().Be(task.Priority);
        response.Status.Should().Be(task.Status);
    }

    [Fact]
    public void Pomodoro_ToResponse_ShouldMapCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var pomodoro = Pomodoro.Create(userId, TimeSpan.FromMinutes(25), SessionType.Work, true);

        // Act
        var response = pomodoro.ToResponse();

        // Assert
        response.Id.Should().Be(pomodoro.Id);
        response.UserId.Should().Be(pomodoro.UserId);
        response.Duration.Should().Be(pomodoro.Duration);
        response.SessionType.Should().Be(pomodoro.SessionType);
        response.IsCompleted.Should().Be(pomodoro.IsCompleted);
    }

    [Fact]
    public void User_ToResponse_ShouldMapCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var response = user.ToResponse();

        // Assert
        response.Id.Should().Be(user.Id);
        response.FirstName.Should().Be(user.FirstName);
        response.LastName.Should().Be(user.LastName);
        response.Email.Should().Be(user.Email);
        response.CreatedAt.Should().Be(user.CreatedAt);
    }
}
