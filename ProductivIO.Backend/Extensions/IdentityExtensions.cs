using Microsoft.AspNetCore.Identity;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.Models;

namespace ProductivIO.Backend.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleManager<RoleManager<UserRole>>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
