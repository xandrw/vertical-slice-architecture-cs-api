using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Config.Swagger;

public static class SwaggerConfig
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                name: "v1",
                info: new()
                {
                    Title = "VS+Clean Architecture API",
                    Version = "v1",
                    Description = "Vertical Slice and Clean Architecture Together"
                });
        });
        services.AddSwaggerExamplesFromAssemblyOf<Program>();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();
    }
}