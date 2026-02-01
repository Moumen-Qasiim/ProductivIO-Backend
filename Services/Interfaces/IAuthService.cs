using ProductivIO.Backend.DTOs.Auth;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginRequest loginRequest);
        Task<AuthResult> RegisterUserAsync(RegisterRequest request);
    }

}