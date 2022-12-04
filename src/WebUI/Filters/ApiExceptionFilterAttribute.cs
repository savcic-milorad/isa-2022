using TransfusionAPI.Application.Common.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TransfusionAPI.Application.Common.Models;

namespace TransfusionAPI.WebUI.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        context.Result = GenerateUnaothorizedAccessProblemDetails();
        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        context.Result = GenerateForbiddenAccessProblemDetails();
        context.ExceptionHandled = true;
    }

    public static ObjectResult GenerateUnaothorizedAccessProblemDetails(Result? result = null, string title = "Unauthorized")
    {
        var problemDetials = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = title,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Extensions = { new KeyValuePair<string, object?>("errors", result?.Errors ?? Enumerable.Empty<string>()) }
        };

        var objectResult = new ObjectResult(problemDetials)
        {
            StatusCode = StatusCodes.Status401Unauthorized,
        };

        return objectResult;
    }

    public static ObjectResult GenerateForbiddenAccessProblemDetails(Result? result = null, string title = "Forbidden")
    {
        var problemDetials = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = title,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            Extensions = { new KeyValuePair<string, object?>("errors", result?.Errors ?? Enumerable.Empty<string>()) }
        };

        var objectResult = new ObjectResult(problemDetials)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        return objectResult;
    }

    public static ObjectResult GenerateBadRequestProblemDetails(Result? result = null, string title = "Bad request")
    {
        var problemDetials = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = title,
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Extensions = { new KeyValuePair<string, object?>("errors", result?.Errors ?? Enumerable.Empty<string>()) }
        };

        var objectResult = new ObjectResult(problemDetials)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        return objectResult;
    }
}
