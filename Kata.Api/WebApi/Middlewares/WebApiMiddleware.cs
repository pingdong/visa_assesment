using System.Net;
using System.Net.Mime;
using FluentValidation;

namespace PingDong.Kata;

public class WebApiMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<WebApiMiddleware> _logger;

    public WebApiMiddleware(RequestDelegate next, ILogger<WebApiMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method != HttpMethods.Get && context.Request.Method != HttpMethods.Post)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;

            var error = new
            {
                Message = "Invalid Http Method",
            };

            await context.Response.WriteAsJsonAsync(error);
        }
        else
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        var error = new
        {
            Message = "Internal Server Error",
        };
        HttpStatusCode statusCode;

        switch (exception)
        {
            case ValidationException ve:
                statusCode = HttpStatusCode.BadRequest;

                error = new
                {
                    Message = ve.Message,
                };

                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;

                error = new
                {
                    Message = "Internal Server Error",
                };

                break;
        }

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        return context.Response.WriteAsJsonAsync(error);
    }
}
