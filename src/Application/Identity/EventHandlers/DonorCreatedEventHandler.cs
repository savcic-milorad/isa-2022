using MediatR;
using Microsoft.Extensions.Logging;
using TransfusionAPI.Domain.Events;

namespace TransfusionAPI.Application.Identity.EventHandlers;

public class DonorCreatedEventHandler : INotificationHandler<DonorCreatedEvent>
{
    private readonly ILogger<DonorCreatedEventHandler> _logger;

    public DonorCreatedEventHandler(ILogger<DonorCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DonorCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TransfusionAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
