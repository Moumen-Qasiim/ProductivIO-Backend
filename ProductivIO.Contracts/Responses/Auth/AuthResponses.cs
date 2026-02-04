namespace ProductivIO.Contracts.Responses.Auth;

public record AuthResponse(
    bool Success,
    string? Message,
    UserResponse? User = null,
    IEnumerable<string>? Errors = null
);

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAt
);
