using Application;
using Infrastructure;
using WebApi;
using WebApi.Config;
using WebApi.Config.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureEnvVariables();

builder.Services.AddControllers();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();