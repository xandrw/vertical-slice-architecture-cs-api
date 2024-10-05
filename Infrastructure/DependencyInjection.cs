using Application;
using Application.Features.Auth;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DatabaseContext>(o => o.UseSqlite(config.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IDataProxy<>), typeof(DataProxy<>));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    }
}