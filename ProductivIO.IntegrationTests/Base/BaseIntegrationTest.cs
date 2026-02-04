using Microsoft.Extensions.DependencyInjection;
using ProductivIO.Infrastructure.Data;
using Xunit;

namespace ProductivIO.IntegrationTests.Base;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestFactory>
{
    protected readonly HttpClient Client;
    protected readonly AppDbContext DbContext;
    protected readonly IServiceProvider ServiceProvider;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        Client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            HandleCookies = true
        });
        ServiceProvider = factory.Services.CreateScope().ServiceProvider;
        DbContext = ServiceProvider.GetRequiredService<AppDbContext>();
        DbContext.Database.EnsureCreated();
    }
}
