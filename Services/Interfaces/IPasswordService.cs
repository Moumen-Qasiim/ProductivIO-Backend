using Microsoft.AspNetCore.Identity;
using ProductivIO.Backend.Models;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(User user, string password);

        PasswordVerificationResult VerifyPassword(User user, string hashPassword, string inputPassword);
    }
}