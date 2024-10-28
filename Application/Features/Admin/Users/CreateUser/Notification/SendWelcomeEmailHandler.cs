using Application.Common.Notification;
using Domain.Users.Events;
using MediatR;

namespace Application.Features.Admin.Users.CreateUser.Notification;

public class SendWelcomeEmailHandler : ConcurrencyHandler, INotificationHandler<Notification<UserCreatedDomainEvent>>
{
    public async Task Handle(Notification<UserCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        await ExecuteAsync(
            async () => await Task.Run(
                () => Console.WriteLine($"Confirmation email sent to {notification.DomainEvent.User.Email}"),
                cancellationToken
            ),
            cancellationToken
        );
    }
}