using Application;
using Infrastructure;
using WebApi;
using WebApi.Config;
using WebApi.Config.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureEnvVariables();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Add features/slices
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
    await app.Services.EnsureDatabaseMigratedAsync();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();