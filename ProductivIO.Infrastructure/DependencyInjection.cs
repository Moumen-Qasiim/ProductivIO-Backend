using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductivIO.Application.Repositories;
using ProductivIO.Infrastructure.Data;
using ProductivIO.Infrastructure.Repositories;

namespace ProductivIO.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isTesting = false)
    {
        if (isTesting || configuration["UseInMemoryDatabase"] == "true")
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("ProductivIO"));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IFlashcardRepository, FlashcardRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IPomodoroRepository, PomodoroRepository>();
        services.AddScoped<IQuizResultRepository, QuizResultRepository>();

        return services;
    }
}
