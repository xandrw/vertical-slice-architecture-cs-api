using Application;
using Infrastructure;
using Microsoft.Extensions.FileProviders;
using WebApi.Config;
using WebApi.Config.Swagger;
using WebApi.Filters;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureEnvVariables();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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
    await app.Services.EnsureDatabaseMigratedAsync();
}

var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "www"));

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = ""
});

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("index.html", new StaticFileOptions
{
    FileProvider = fileProvider
});

app.Run();