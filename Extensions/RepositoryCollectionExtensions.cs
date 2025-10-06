using Microsoft.Extensions.DependencyInjection;
using ProductivIOBackend.Repositories;
using ProductivIOBackend.Repositories.Interfaces;

namespace ProductivIOBackend.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IPomodoroRepository, PomodoroRepository>();
            services.AddScoped<IFlashcardRepository, FlashcardRepository>();
            services.AddScoped<IQuizResultRepository, QuizResultRepository>();

            return services;
        }
    }
}
