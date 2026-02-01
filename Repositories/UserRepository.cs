using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Repositories.Interfaces;

namespace ProductivIO.Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserRepository(AppDbContext db)
        {
            _db = db;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User?> GetUserAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            // Find existing user in database
            var existingUser = await _db.Users.FindAsync(user.Id);

            if (existingUser == null)
                return null;

            // Update password
            existingUser.Password = _passwordHasher.HashPassword(existingUser, user.Password);

            // Mark entity as modified
            _db.Users.Update(existingUser);
            await _db.SaveChangesAsync();

            return existingUser;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
