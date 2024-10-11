using Application;
using Infrastructure;
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

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();