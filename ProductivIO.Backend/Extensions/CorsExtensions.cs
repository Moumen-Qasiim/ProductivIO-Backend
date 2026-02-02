namespace ProductivIO.Backend.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddDevCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("DevCors", p =>
                    p.WithOrigins("http://localhost:5171", "http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            return services;
        }
    }
}
