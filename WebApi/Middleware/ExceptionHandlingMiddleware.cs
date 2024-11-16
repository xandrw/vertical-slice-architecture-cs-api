using System.Net;
using System.Text.Json;
using Application.Common.Http.Exceptions;
using Application.Common.Http.Responses;

namespace WebApi.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (UnprocessableHttpException e)
        {
            logger.LogError(e, "{Message}", e.Message);
            await HandleUnprocessableHttpExceptionAsync(context, e);
        }
        catch (HttpException e)
        {
            logger.LogError(e, "{Message}", e.Message);
            await HandleHttpExceptionAsync(context, e);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An unhandled exception occurred.");
            await HandleExceptionAsync(context);
        }
    }
    
    private async Task HandleUnprocessableHttpExceptionAsync(HttpContext context, UnprocessableHttpException e)
    {
        context.Response.StatusCode = e.StatusCode;
        context.Response.ContentType = "application/json";
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        
        var result = JsonSerializer.Serialize(new UnprocessableEntityResponse
        {
            Error = e.Message,
            Status = e.StatusCode,
            Errors = e.Errors
        }, options);

        await context.Response.WriteAsync(result);
    }
    
    private async Task HandleHttpExceptionAsync(HttpContext context, HttpException e)
    {
        context.Response.StatusCode = e.StatusCode;
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new { error = e.Message });

        await context.Response.WriteAsync(result);
    }
    
    private async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });

        await context.Response.WriteAsync(result);
    }
}