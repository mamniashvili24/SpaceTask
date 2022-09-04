using API.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class MiddlewareExtentions
{
    public static void ErrorExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}