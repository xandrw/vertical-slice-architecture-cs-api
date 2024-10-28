using System.Collections.Concurrent;
using Domain;
using MediatR;
using static System.Activator;

namespace Application.Common.Notification;

public class EventPublisher(IMediator mediator)
{
    private static readonly ConcurrentDictionary<Type, Type> NotificationTypeCache = new();

    public async Task PublishDomainEvent(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var notificationType = NotificationTypeCache.GetOrAdd(
            domainEvent.GetType(),
            t => typeof(Notification<>).MakeGenericType(t)
        );
        var notification = CreateInstance(notificationType, domainEvent);

        if (notification is null) return;

        await BaseDomainEntity.PublishEvent(
            domainEvent,
            async _ => await mediator.Publish((INotification)notification, cancellationToken)
        );
    }
}