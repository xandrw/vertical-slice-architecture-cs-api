using Domain;
using MediatR;

namespace Application.Common.Notification;

public class Notification<TDomainEvent>(TDomainEvent domainEvent) : INotification where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}