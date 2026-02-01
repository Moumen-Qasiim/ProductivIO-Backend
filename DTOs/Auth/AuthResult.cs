using ProductivIO.Backend.DTOs.User;

namespace ProductivIO.Backend.DTOs.Auth
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserResponse? User { get; set; }
        public string? Token { get; set; }

        public static AuthResult Failure(string message) =>
            new() { Success = false, Message = message };

        public static AuthResult Successful(Models.User user, string token) =>
            new()
            {
                Success = true,
                Message = "Success",
                User = new UserResponse(user),
                Token = token
            };
    }
}