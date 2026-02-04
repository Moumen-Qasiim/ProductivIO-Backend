using Moq;
using FluentAssertions;
using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services;
using ProductivIO.Contracts.Requests.Quiz;
using ProductivIO.Domain.Entities;
using Xunit;

namespace ProductivIO.UnitTests.Services;

public class QuizServiceTests
{
    private readonly Mock<IQuizRepository> _quizRepositoryMock;
    private readonly Mock<IQuizResultRepository> _quizResultRepositoryMock;
    private readonly QuizService _quizService;

    public QuizServiceTests()
    {
        _quizRepositoryMock = new Mock<IQuizRepository>();
        _quizResultRepositoryMock = new Mock<IQuizResultRepository>();
        _quizService = new QuizService(_quizRepositoryMock.Object, _quizResultRepositoryMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateAsync_ShouldReturnQuizResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateQuizRequest("Quiz Title", "Quiz Desc");
        _quizRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Quiz>()))
            .ReturnsAsync((Quiz q) => q);

        // Act
        var result = await _quizService.CreateAsync(request, userId);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("Quiz Title");
        result.UserId.Should().Be(userId);
    }

    [Fact]
    public async System.Threading.Tasks.Task AddQuestionAsync_ShouldReturnQuestionResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var quizId = Guid.NewGuid();
        var quiz = Quiz.Create(userId, "Title", "Desc");
        var request = new CreateQuizQuestionRequest("New Question", new List<CreateQuizAnswerRequest>());

        _quizRepositoryMock.Setup(x => x.GetByIdAsync(quizId, userId)).ReturnsAsync(quiz);
        _quizRepositoryMock.Setup(x => x.AddQuestionAsync(It.IsAny<QuizQuestion>()))
            .ReturnsAsync((QuizQuestion q) => q);

        // Act
        var result = await _quizService.AddQuestionAsync(quizId, request, userId);

        // Assert
        result.Should().NotBeNull();
        result!.Question.Should().Be("New Question");
    }
}
