using FluentAssertions;
using ProductivIO.Domain.Entities;
using Xunit;

namespace ProductivIO.UnitTests.Domain;

public class NoteTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnNote()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var title = "Test Note";
        var content = "Test Content";

        // Act
        var note = Note.Create(userId, title, content);

        // Assert
        note.Should().NotBeNull();
        note.UserId.Should().Be(userId);
        note.Title.Should().Be(title);
        note.Content.Should().Be(content);
        note.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Create_WithEmptyTitle_ShouldThrowArgumentException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var title = "";

        // Act
        Action act = () => Note.Create(userId, title, "Content");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*Title cannot be empty*");
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateNote()
    {
        // Arrange
        var note = Note.Create(Guid.NewGuid(), "Old Title", "Old Content");
        var newTitle = "New Title";
        var newContent = "New Content";

        // Act
        note.Update(newTitle, newContent);

        // Assert
        note.Title.Should().Be(newTitle);
        note.Content.Should().Be(newContent);
        note.UpdatedAt.Should().NotBeNull();
    }
}
