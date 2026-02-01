using ProductivIO.Backend.Models;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
