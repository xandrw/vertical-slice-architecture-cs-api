using System.Net;
using System.Text.Json;
using Application;

namespace WebApi;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (HttpException e)
        {
            logger.LogError(e, "{Message}", e.Message);
            await HandleHttpException(context, e);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An unhandled exception occurred.");
            await HandleGlobalExceptionAsync(context);
        }
    }

    private Task HandleHttpException(HttpContext context, HttpException e)
    {
        context.Response.StatusCode = e.StatusCode;
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new { error = e.Message });

        return context.Response.WriteAsync(result);
    }

    private Task HandleGlobalExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });

        return context.Response.WriteAsync(result);
    }
}