using Microsoft.AspNetCore.Identity;
using ProductivIO.Backend.DTOs.Auth;
using ProductivIO.Backend.DTOs.User;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Repositories.Interfaces;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            // Find user
            var user = await _userRepository.GetUserAsync(request.Email);
            if (user == null)
                return AuthResult.Failure("Email does not exist.");

            // Verify password
            var result = _passwordService.VerifyPassword(user, user.Password, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return AuthResult.Failure("Incorrect password.");

            // Rehash password to make password stronger over time
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = _passwordService.HashPassword(user, request.Password);
                await _userRepository.UpdateUserAsync(user);
            }

            // Generate JWT
            var token = _tokenService.GenerateToken(user);

            return AuthResult.Successful(user, token);
        }   

        public async Task<AuthResult> RegisterUserAsync(RegisterRequest request)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetUserAsync(request.Email);
            if (existingUser != null)
                return AuthResult.Failure("Email already registered.");

            // Create new user and hash password
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CreatedAt = DateTime.Now
            };

            newUser.Password = _passwordService.HashPassword(newUser, request.Password);

            // Save to database
            var savedUser = await _userRepository.AddUserAsync(newUser);
            if (savedUser == null)
                return AuthResult.Failure("User could not be created.");

            return new AuthResult { Success = true, Message = "User created successfully.", User = new UserResponse(savedUser) };
        }
    }
}
