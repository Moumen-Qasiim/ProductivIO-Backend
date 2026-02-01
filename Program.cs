using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.Extensions;
using ProductivIO.Backend.Validations;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

// Core MVC & Validation
builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddEndpointsApiExplorer();

// Modularized Config
builder.Services.AddAppDatabase(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDevCors();
builder.Services.AddRepositories();
builder.Services.AddAppServices();

builder.Services.AddAuthorization();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductivIO API v1");
        c.RoutePrefix = string.Empty;
    });
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
app.UseCors("DevCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}


app.Run();
