using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Config.Swagger;

public static class SwaggerConfig
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddSwaggerExamplesFromAssemblyOf<Program>();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();
    }
}