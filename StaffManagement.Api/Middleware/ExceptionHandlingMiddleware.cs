using StaffManagement.Application.Exceptions;
using StaffManagement.Domain.Abstractions;

namespace StaffManagement.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Este error es una excepcion");

            if(ex is ValidationException validationException)
            {   
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(validationException.Errors);
                return;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            var error = new Error("UnexpectedError", _env.IsDevelopment() ? ex.ToString() : "Ha ocurrido un error inesperado");

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
