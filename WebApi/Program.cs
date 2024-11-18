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
            .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
            .Enrich.FromLogContext()
            .CreateLogger();

        try
        {
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
            }

            await app.Services.EnsureDatabaseMigratedAsync();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            // app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Application failed to start.");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}