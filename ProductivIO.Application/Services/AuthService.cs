using Bogus;
using Microsoft.AspNetCore.Identity;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Application.Mapping;
using ProductivIO.Contracts.Requests.Auth;
using ProductivIO.Contracts.Responses.Auth;
using ProductivIO.Domain.Entities;
using System.Security.Claims;

namespace ProductivIO.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly Faker _faker = new();

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResponse(false, "Invalid credentials.");

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            return new AuthResponse(true, "Login successful.", user.ToResponse());
        }

        if (result.IsLockedOut)
            return new AuthResponse(false, "Account locked out.");

        return new AuthResponse(false, "Invalid credentials.");
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            UserName = _faker.Internet.UserName(request.FirstName, request.LastName) + _faker.Random.Number(100, 999),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return new AuthResponse(false, "Registration failed.", null, result.Errors.Select(e => e.Description));

        await _signInManager.SignInAsync(user, isPersistent: false);
        return new AuthResponse(true, "User registered successfully.", user.ToResponse());
    }

    public async Task<UserResponse?> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        return user?.ToResponse();
    }
}
