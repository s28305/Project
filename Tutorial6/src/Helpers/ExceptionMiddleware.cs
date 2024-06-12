using System.Net;
using Tutorial6.DTO;

namespace Tutorial6.Helpers;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "An unexpected error occurred.");

        var response = exception switch
        {
            ApplicationException _ => new ExceptionDto(HttpStatusCode.BadRequest, "Application exception has occurred."),
            KeyNotFoundException _ => new ExceptionDto(HttpStatusCode.NotFound, "The requested endpoint was not found."),
            UnauthorizedAccessException _ => new ExceptionDto(HttpStatusCode.Unauthorized, "Unauthorized."),
            _ => new ExceptionDto(HttpStatusCode.InternalServerError, "Internal server error. Please try again later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}