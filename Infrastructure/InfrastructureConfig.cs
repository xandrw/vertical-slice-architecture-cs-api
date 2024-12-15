using System.Data.Common;
using Application.Features.Auth;
using Application.Features.GetCurrentDateTime;
using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.Http.PostmanEcho;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureConfig
{
    private static DbConnection? _sharedInMemoryConnection;
    
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        if (config["ASPNETCORE_ENVIRONMENT"] == "Test")
        {
            _sharedInMemoryConnection ??= new SqliteConnection("Data Source=TestDatabase;Mode=Memory;Cache=Shared");
            _sharedInMemoryConnection.Open();

            services.AddDbContext<DatabaseContext>(o => o.UseSqlite(_sharedInMemoryConnection));
        }
        else
        {
            services.AddDbContext<DatabaseContext>(o => o.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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

        if (!databaseContext.Database.IsSqlite()) return;

        await databaseContext.Database.EnsureCreatedAsync();
        await databaseContext.Database.MigrateAsync();
    }
}