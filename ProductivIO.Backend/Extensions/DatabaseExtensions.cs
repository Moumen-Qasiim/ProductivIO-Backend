using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;

namespace ProductivIO.Backend.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddAppDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
