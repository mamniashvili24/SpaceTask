using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CommonTypes.Implementations;

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
            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            var errorMessage = JsonConvert.SerializeObject(new DataResponse(ex), Formatting.Indented);
            await httpContext.Response.WriteAsync(errorMessage);
        }
    }
}