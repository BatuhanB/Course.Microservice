using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Course.Invoice.Api.Middlewares;

public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var result = new Models.ProblemDetails();

        if (exception.GetType() == typeof(FluentValidation.ValidationException))
        {
            var errors = ((FluentValidation.ValidationException)exception).Errors.Select(x => x.PropertyName).ToList();

            result = new Models.ProblemDetails
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Type = exception.GetType().Name,
                Title = "Validation error",
                Detail = string.Join(" , ", errors),
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            };
            _logger.LogError(exception, $"Exception occured : {exception.Message}");
        }
        else if (exception.GetType() == typeof(ArgumentNullException))
        {
            result = new Models.ProblemDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Type = exception.GetType().Name,
                Title = "An unexpected error occurred",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            };
            _logger.LogError(exception, $"Exception occured : {exception.Message}");
        }
        else
        {
            result = new Models.ProblemDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = "An unexpected error occurred",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };
            _logger.LogError(exception, $"Exception occured : {exception.Message}");
        }
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);


        return true;
    }
}