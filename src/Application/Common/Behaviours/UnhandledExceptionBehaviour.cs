using MediatR;
using Microsoft.Extensions.Logging;

namespace TransfusionAPI.Application.Common.Behaviours;

/// <summary>
/// Wrapper for other behaviours or application code which catches any unhandled exceptions, in such a case, exception is caught, logged as error and is rethrown.
/// </summary>
public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "TransfusionAPI Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
