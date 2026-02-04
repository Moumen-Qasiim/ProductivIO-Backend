using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ProductivIO.Contracts.Requests.Auth;
using ProductivIO.Contracts.Responses.Auth;
using ProductivIO.IntegrationTests.Base;
using Xunit;

namespace ProductivIO.IntegrationTests;

public class AuthIntegrationTests : BaseIntegrationTest
{
    public AuthIntegrationTests(IntegrationTestFactory factory) : base(factory) { }

    [Fact]
    public async System.Threading.Tasks.Task Register_ShouldCreateUserAndReturnSuccess()
    {
        // Arrange
        var request = new RegisterRequest("Jane", "Doe", "jane@example.com", "Password123!");

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
        result.Message.Should().Contain("User registered successfully");
    }

    [Fact]
    public async System.Threading.Tasks.Task Login_ShouldReturnSuccess_WhenCredentialsAreValid()
    {
        // Arrange
        var regRequest = new RegisterRequest("John", "Doe", "john@example.com", "Password123!");
        await Client.PostAsJsonAsync("/api/auth/register", regRequest);

        var loginRequest = new LoginRequest("john@example.com", "Password123!");

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
    }
}
