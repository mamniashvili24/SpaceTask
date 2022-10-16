using API.ErrorHandling;

namespace API.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.ToProblemDetails();
            await httpContext.Response.WriteAsJsonAsync(errorMessage);
        }
    }
}