using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DatabaseContext>(o => o.UseSqlite(config.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}