using Application;
using Infrastructure;
using Serilog;
using WebApi.Config;
using WebApi.Config.Swagger;
using WebApi.Filters;
using WebApi.Middleware;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            Log.Information("Starting application");
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.ConfigureEnvVariables();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            builder.WebHost.UseUrls("http://localhost:5255");

            builder.Services
                .AddControllers(options => options.Filters.Add<ValidateModelFilter>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            builder.Services.AddHttpContextAccessor();
            builder.Services.ConfigureAuth(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwagger();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerDocumentation();
                app.UseDeveloperExceptionPage();
                await app.Services.EnsureDatabaseMigratedAsync();
            }

            app.Use(async (_, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Unhandled exception");
                    throw;
                }
            });

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            // app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}