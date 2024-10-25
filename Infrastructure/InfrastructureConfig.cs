using Application.Features.Auth;
using Application.Features.GetCurrentDateTime;
using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.Http.PostmanEcho;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureConfig
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DatabaseContext>(o => o.UseSqlite(config.GetConnectionString("DefaultConnection")));
        // TODO: Maybe convert to Repository pattern
        services.AddScoped(typeof(IDbProxy<>), typeof(DbProxy<>));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, HmacSha512PasswordHasher>();

        services.AddHttpClient<IPostmanEchoTimeClient, PostmanEchoTimeClient>(client =>
        {
            client.BaseAddress = new Uri("https://postman-echo.com/time");
        });
    }

    public static async Task EnsureDatabaseMigratedAsync(this IServiceProvider services)
    {
        await using var serviceScope = services.CreateAsyncScope();
        await using var databaseContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await databaseContext.Database.MigrateAsync();
    }
}