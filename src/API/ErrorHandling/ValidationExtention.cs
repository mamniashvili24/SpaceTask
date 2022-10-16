using Microsoft.AspNetCore.Mvc;

namespace API.ErrorHandling;

public static class ValidationExtention
{
    public static ValidationProblemDetails ToProblemDetails(this Exception ex)
    {
        var error = new ValidationProblemDetails
        {
            Status = 400,
            Detail = ex.Message,
            Title = "General exeption happend",
            Type = "https://www.rfc-editor.org/rfc/rfc5378.html"
        };

        return error;
    }
}
