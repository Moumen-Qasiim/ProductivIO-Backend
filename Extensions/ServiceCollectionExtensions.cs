using Microsoft.Extensions.DependencyInjection;
using ProductivIOBackend.Services;
using ProductivIOBackend.Services.Interfaces;

namespace ProductivIOBackend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IPomodoroService, PomodoroService>();
            services.AddScoped<IFlashcardService, FlashcardService>();
            services.AddScoped<IQuizResultService, QuizResultService>();

            return services;
        }
    }
}
