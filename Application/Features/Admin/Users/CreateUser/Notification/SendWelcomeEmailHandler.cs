using Application.Common.Notification;
using Domain.Users.Events;
using MediatR;

namespace Application.Features.Admin.Users.CreateUser.Notification;

public class SendWelcomeEmailHandler : INotificationHandler<Notification<UserCreatedDomainEvent>>
{
    private static readonly SemaphoreSlim Semaphore = new(initialCount: 1, maxCount: 1);

    public async Task Handle(Notification<UserCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        await Semaphore.WaitAsync(cancellationToken);

        try
        {
            await Task.Run(
                () => Console.WriteLine($"Confirmation email sent to {notification.DomainEvent.User.Email}"),
                cancellationToken
            );
        }
        finally
        {
            Semaphore.Release();
        }
    }
}