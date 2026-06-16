using System.Text.Json;
using Validador.Domain.Exceptions;

namespace Validador.API.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (PasswordValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var body = JsonSerializer.Serialize(new
            {
                isValid = false,
                errors = ex.Errors
            });

            await context.Response.WriteAsync(body);
        }
    }
}
