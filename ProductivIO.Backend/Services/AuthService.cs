using Bogus;
using Microsoft.AspNetCore.Identity;
using ProductivIO.Backend.DTOs.Auth;
using ProductivIO.Backend.DTOs.User;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
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

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return AuthResult.Failure("Invalid email or password.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new AuthResult { Success = true, Message = "Login successful", User = new UserResponse(user) };
            }

            if (result.IsLockedOut)
            {
                return AuthResult.Failure("Account locked out.");
            }

            return AuthResult.Failure("Invalid email or password.");
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterRequest request)
        {
            var user = new User
            {
                UserName = _faker.Internet.UserName(request.FirstName, request.LastName) + _faker.Random.Number(100, 999),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return new AuthResult { Success = true, Message = "User created and logged in successfully.", User = new UserResponse(user) };
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return AuthResult.Failure(errors);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserResponse?> GetCurrentUserAsync(System.Security.Claims.ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            return user != null ? new UserResponse(user) : null;
        }
    }
}
