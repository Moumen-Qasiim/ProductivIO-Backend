using FluentAssertions;
using ProductivIO.Application.Mapping;
using ProductivIO.Domain.Entities;
using Xunit;

namespace ProductivIO.UnitTests.Mapping;

public class NoteMapperTests
{
    [Fact]
    public void ToResponse_ShouldMapNoteToNoteResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var note = Note.Create(userId, "Test Title", "Test Content");

        // Act
        var response = note.ToResponse();

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(note.Id);
        response.UserId.Should().Be(note.UserId);
        response.Title.Should().Be(note.Title);
        response.Content.Should().Be(note.Content);
        response.CreatedAt.Should().Be(note.CreatedAt);
    }
}
