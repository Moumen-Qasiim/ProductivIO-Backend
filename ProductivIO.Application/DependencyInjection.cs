using Microsoft.Extensions.DependencyInjection;
using ProductivIO.Application.Services;
using ProductivIO.Application.Services.Interfaces;

namespace ProductivIO.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IFlashcardService, FlashcardService>();
        services.AddScoped<IQuizService, QuizService>();
        services.AddScoped<IPomodoroService, PomodoroService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
