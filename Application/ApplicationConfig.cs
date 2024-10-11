using Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationConfig
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationConfig).Assembly;
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
        services.AddScoped<AuthenticatedUser>();
    }
}