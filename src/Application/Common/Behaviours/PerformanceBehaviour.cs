using System.Diagnostics;
using TransfusionAPI.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TransfusionAPI.Application.Common.Behaviours;

/// <summary>
/// Measure elapsed time for request roundtrip, if over threshold output to log
/// </summary>
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
{
    private static readonly int Threshold_ms = 500;

    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        var userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        var isLongRunningRequest = elapsedMilliseconds > Threshold_ms;

        _logger.Log(isLongRunningRequest ? LogLevel.Warning : LogLevel.Information,
            "TransfusionAPI Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}", 
            requestName, elapsedMilliseconds, userId, userName, request);

        return response;
    }
}
