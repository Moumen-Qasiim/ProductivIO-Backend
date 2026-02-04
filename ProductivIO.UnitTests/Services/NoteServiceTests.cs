using Moq;
using FluentAssertions;
using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services;
using ProductivIO.Contracts.Requests.Notes;
using ProductivIO.Domain.Entities;
using Xunit;

namespace ProductivIO.UnitTests.Services;

public class NoteServiceTests
{
    private readonly Mock<INoteRepository> _noteRepositoryMock;
    private readonly NoteService _noteService;

    public NoteServiceTests()
    {
        _noteRepositoryMock = new Mock<INoteRepository>();
        _noteService = new NoteService(_noteRepositoryMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetAllAsync_ShouldReturnNoteResponses()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var notes = new List<Note>
        {
            Note.Create(userId, "Title 1", "Content 1"),
            Note.Create(userId, "Title 2", "Content 2")
        };
        _noteRepositoryMock.Setup(x => x.GetAllAsync(userId)).ReturnsAsync(notes);

        // Act
        var result = await _noteService.GetAllAsync(userId);

        // Assert
        result.Should().HaveCount(2);
        result.First().Title.Should().Be("Title 1");
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateAsync_ShouldReturnCreatedNoteResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateNoteRequest("New Note", "New Content");
        _noteRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Note>()))
            .ReturnsAsync((Note n) => n);

        // Act
        var result = await _noteService.CreateAsync(request, userId);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("New Note");
        result.UserId.Should().Be(userId);
    }
}
