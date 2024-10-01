using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
        services.AddSwaggerExamplesFromAssemblyOf(typeof(DependencyInjection));

        return services;
    }
}