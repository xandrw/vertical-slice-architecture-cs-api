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
            LogValidationErrors(e);
            
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

    private static async Task HandleUnprocessableHttpExceptionAsync(HttpContext context, UnprocessableHttpException e)
    {
        context.Response.StatusCode = e.StatusCode;
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(
            new UnprocessableEntityResponse
            {
                Error = e.Message,
                Status = e.StatusCode,
                Errors = e.Errors
            },
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                AllowTrailingCommas = true
            }
        );

        await context.Response.WriteAsync(result);
    }

    private void LogValidationErrors(UnprocessableHttpException e)
    {
        logger.LogError(e, "{Message}", e.Message);
        
        foreach ((string fieldName, string[]? validationErrors) in e.Errors)
        {
            if (validationErrors is null || validationErrors.Length <= 0) continue;
            
            foreach (var validationError in validationErrors)
            {
                logger.LogError($"{fieldName}: {validationError}");
            }
        }
    }

    private static async Task HandleHttpExceptionAsync(HttpContext context, HttpException e)
    {
        context.Response.StatusCode = e.StatusCode;
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new { error = e.Message });

        await context.Response.WriteAsync(result);
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });

        await context.Response.WriteAsync(result);
    }
}