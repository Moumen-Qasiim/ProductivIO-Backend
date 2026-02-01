using Microsoft.AspNetCore.Identity;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyPassword(User user, string hashPassword, string inputPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashPassword, inputPassword);
        }
    }
}