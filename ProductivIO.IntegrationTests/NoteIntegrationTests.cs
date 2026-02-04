using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ProductivIO.Contracts.Requests.Notes;
using ProductivIO.Contracts.Responses.Notes;
using ProductivIO.Contracts.Requests.Auth;
using ProductivIO.Contracts.Responses.Auth;
using ProductivIO.IntegrationTests.Base;
using Xunit;

namespace ProductivIO.IntegrationTests;

public class NoteIntegrationTests : BaseIntegrationTest
{
    public NoteIntegrationTests(IntegrationTestFactory factory) : base(factory) { }

    private async System.Threading.Tasks.Task AuthenticateAsync()
    {
        var email = $"test_{Guid.NewGuid()}@example.com";
        var regRequest = new RegisterRequest("Test", "User", email, "Password123!");
        await Client.PostAsJsonAsync("/api/auth/register", regRequest);

        var loginRequest = new LoginRequest(email, "Password123!");
        await Client.PostAsJsonAsync("/api/auth/login", loginRequest);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateNote_ShouldReturnCreated()
    {
        // Arrange
        await AuthenticateAsync();
        var request = new CreateNoteRequest("My Test Note", "Some content");

        // Act
        var response = await Client.PostAsJsonAsync("/api/notes", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<NoteResponse>();
        result.Should().NotBeNull();
        result!.Title.Should().Be("My Test Note");
    }

    [Fact]
    public async System.Threading.Tasks.Task GetAllNotes_ShouldReturnOk()
    {
        // Arrange
        await AuthenticateAsync();

        // Act
        var response = await Client.GetAsync("/api/notes");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
