using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivIO.Backend.DTOs.Auth;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        // POST: api/Auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            _logger.LogInformation("Login attempt for email: {Email}", loginRequest.Email);
            var result = await _authService.LoginAsync(loginRequest);

            if (!result.Success)
            {
                _logger.LogWarning("Failed login attempt for email {Email}: {Message}", loginRequest.Email, result.Message);
                return Unauthorized(new { success = false, message = result.Message });
            }

            _logger.LogInformation("User {Email} logged in successfully", loginRequest.Email);
            return Ok(new { message = result.Message, user = result.User });
        }

        // POST: api/Auth/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            _logger.LogInformation("Register attempt for email: {Email}", registerRequest.Email);
            var result = await _authService.RegisterUserAsync(registerRequest);
            
            if (!result.Success)
            {
                _logger.LogWarning("Failed register attempt for email {Email}: {Message}", registerRequest.Email, result.Message);
                return BadRequest(result.Message);
            }
            
            _logger.LogInformation("User {Email} registered successfully", registerRequest.Email);
            return Ok(new { message = result.Message, user = result.User });
        }
        
        // POST: api/Auth/logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            _logger.LogInformation("User logged out.");
            return Ok(new { message = "Logged out successfully" });
        }

        // GET: api/Auth/validate
        [Authorize] 
        [HttpGet("validate")]
        public async Task<IActionResult> Validate()
        {
            var user = await _authService.GetCurrentUserAsync(User);
            if (user == null) return Unauthorized();
            
            return Ok(new { valid = true, user });
        }
    }
}
