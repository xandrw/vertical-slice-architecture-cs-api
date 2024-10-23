using Application.Common.Notification;
using Domain.Users.Events;
using MediatR;

namespace Application.Features.Admin.Users.CreateUser.Notification;

public class SendWelcomeEmailHandler : INotificationHandler<Notification<UserCreatedDomainEvent>>
{
    public async Task Handle(Notification<UserCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        await Task.Run(
            () => Console.WriteLine($"Confirmation email sent to {notification.DomainEvent.User.Email}"),
            cancellationToken
        );
    }
}