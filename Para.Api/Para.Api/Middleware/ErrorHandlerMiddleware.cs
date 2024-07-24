using FluentValidation;
using Para.Api.Service.Logger;
using Para.Base.Response;
using PBCW2.Bussiness.Exceptions;
using System.Text.Json;

namespace Para.Api.Middleware;


public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILoggerService logger;

    public ErrorHandlerMiddleware(RequestDelegate next,ILoggerService logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        logger.LogInfo($"Handling request: {context.Request.Method} {context.Request.Path}");
        
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }

        stopwatch.Stop();

        var response = context.Response;
        var logResponseMessage = $"Response Status Code: {response.StatusCode}, Response Time: {stopwatch.ElapsedMilliseconds} ms";
        logger.LogInfo(logResponseMessage);

    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        int statusCode;
        string message;

        switch (ex)
        {
            case NotFoundException _:
                statusCode = StatusCodes.Status404NotFound;
                message = ex.Message;
                break;
            case ValidationException _:
                statusCode = StatusCodes.Status400BadRequest;
                message = ex.Message;
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        logger.LogError("An error occurred while processing the request.", ex);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(
            JsonSerializer.Serialize(new ApiResponse(message)));

    }
}