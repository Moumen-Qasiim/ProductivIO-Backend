using Moq;
using FluentAssertions;
using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services;
using ProductivIO.Contracts.Requests.Flashcards;
using ProductivIO.Domain.Entities;
using Xunit;

namespace ProductivIO.UnitTests.Services;

public class FlashcardServiceTests
{
    private readonly Mock<IFlashcardRepository> _flashcardRepositoryMock;
    private readonly FlashcardService _flashcardService;

    public FlashcardServiceTests()
    {
        _flashcardRepositoryMock = new Mock<IFlashcardRepository>();
        _flashcardService = new FlashcardService(_flashcardRepositoryMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateAsync_ShouldReturnFlashcardResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateFlashcardRequest("Title", "Description");
        _flashcardRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Flashcard>()))
            .ReturnsAsync((Flashcard f) => f);

        // Act
        var result = await _flashcardService.CreateAsync(request, userId);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("Title");
        result.UserId.Should().Be(userId);
    }

    [Fact]
    public async System.Threading.Tasks.Task AddQuestionAsync_ShouldReturnQuestionResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var flashcardId = Guid.NewGuid();
        var flashcard = Flashcard.Create(userId, "Title", "Desc");
        var request = new CreateFlashcardQuestionRequest("Question", "Hint");

        _flashcardRepositoryMock.Setup(x => x.GetByIdAsync(flashcardId, userId)).ReturnsAsync(flashcard);
        _flashcardRepositoryMock.Setup(x => x.AddQuestionAsync(It.IsAny<FlashcardQuestion>()))
            .ReturnsAsync((FlashcardQuestion q) => q);

        // Act
        var result = await _flashcardService.AddQuestionAsync(flashcardId, request, userId);

        // Assert
        result.Should().NotBeNull();
        result!.Question.Should().Be("Question");
        result.Hint.Should().Be("Hint");
    }
}
