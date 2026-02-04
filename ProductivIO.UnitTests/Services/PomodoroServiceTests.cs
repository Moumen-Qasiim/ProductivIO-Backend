using Moq;
using FluentAssertions;
using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services;
using ProductivIO.Contracts.Requests.Pomodoro;
using ProductivIO.Domain.Entities;
using ProductivIO.Contracts.Enums;
using Xunit;

namespace ProductivIO.UnitTests.Services;

public class PomodoroServiceTests
{
    private readonly Mock<IPomodoroRepository> _pomodoroRepositoryMock;
    private readonly PomodoroService _pomodoroService;

    public PomodoroServiceTests()
    {
        _pomodoroRepositoryMock = new Mock<IPomodoroRepository>();
        _pomodoroService = new PomodoroService(_pomodoroRepositoryMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateAsync_ShouldReturnPomodoroResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreatePomodoroRequest(TimeSpan.FromMinutes(25), SessionType.Work, false);
        _pomodoroRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Pomodoro>()))
            .ReturnsAsync((Pomodoro p) => p);

        // Act
        var result = await _pomodoroService.CreateAsync(request, userId);

        // Assert
        result.Should().NotBeNull();
        result!.SessionType.Should().Be(SessionType.Work);
        result.Duration.Should().Be(TimeSpan.FromMinutes(25));
        result.UserId.Should().Be(userId);
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateAsync_ShouldReturnTrue_WhenSessionExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        var session = Pomodoro.Create(userId, TimeSpan.FromMinutes(25), SessionType.Work, false);
        var request = new UpdatePomodoroRequest(TimeSpan.FromMinutes(30), SessionType.ShortBreak, true);

        _pomodoroRepositoryMock.Setup(x => x.GetByIdAsync(sessionId, userId)).ReturnsAsync(session);
        _pomodoroRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Pomodoro>())).ReturnsAsync(true);

        // Act
        var result = await _pomodoroService.UpdateAsync(sessionId, request, userId);

        // Assert
        result.Should().BeTrue();
        session.SessionType.Should().Be(SessionType.ShortBreak);
        session.IsCompleted.Should().BeTrue();
    }
}
