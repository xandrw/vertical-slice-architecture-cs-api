using Domain;
using MediatR;
using static System.Activator;

namespace Application.Common.Notification;

public class Publisher(IMediator mediator)
{
    public async Task PublishDomainEvent(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var notificationType = typeof(Notification<>).MakeGenericType(domainEvent.GetType());
        var notification = CreateInstance(notificationType, domainEvent);

        if (notification is null) return;

        await BaseDomainEntity.PublishEvent(
            domainEvent,
            async _ => await mediator.Publish((INotification)notification, cancellationToken)
        );
    }
}