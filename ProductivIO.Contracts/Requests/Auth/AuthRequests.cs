using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Contracts.Requests.Auth;

public record LoginRequest(
    [Required] [EmailAddress] string Email,
    [Required] string Password
);

public record RegisterRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] [EmailAddress] string Email,
    [Required] [MinLength(8)] string Password // TODO: password policy should be assigned in the identity configuration
);
