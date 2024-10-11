using Application;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Config.Swagger;

public static class SwaggerConfig
{
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        
        return app;
    }
    
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
        services.AddSwaggerExamplesFromAssemblyOf(typeof(ApplicationConfig));
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();
    }
}