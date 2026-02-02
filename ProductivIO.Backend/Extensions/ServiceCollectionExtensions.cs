using ProductivIO.Backend.Services;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
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
