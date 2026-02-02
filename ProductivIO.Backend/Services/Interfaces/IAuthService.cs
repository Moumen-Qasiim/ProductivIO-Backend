using ProductivIO.Backend.DTOs.Auth;
using ProductivIO.Backend.DTOs.User;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginRequest loginRequest);
        Task<AuthResult> RegisterUserAsync(RegisterRequest request);
        Task LogoutAsync();
        Task<UserResponse?> GetCurrentUserAsync(System.Security.Claims.ClaimsPrincipal userPrincipal);
    }
}