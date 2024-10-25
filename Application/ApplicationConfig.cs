using Application.Common;
using Application.Common.Notification;
using Application.Features.Admin.Pages.Publication;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationConfig
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ApplicationConfig).Assembly));
        services.AddScoped<AuthenticatedUser>();
        services.AddScoped<EventPublisher>();
        services.AddScoped<PagePublicationManager>();
    }
}