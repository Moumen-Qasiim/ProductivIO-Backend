using ProductivIO.Backend.Models;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string email);
        Task<User?> UpdateUserAsync(User user);
        Task<User?> AddUserAsync(User user);
    }
}