namespace Application.Common.Notification;

public abstract class ConcurrencyHandler
{
    private static readonly SemaphoreSlim Semaphore = new(initialCount: 1, maxCount: 1);

    protected async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        await Semaphore.WaitAsync(cancellationToken);

        try
        {
            await action();
        }
        finally
        {
            Semaphore.Release();
        }
    }
}