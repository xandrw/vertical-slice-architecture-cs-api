namespace Domain;

public interface IEntity;

public class BaseDomainEntity : IEntity
{
    protected BaseDomainEntity() {}
    
    public static async Task PublishEvent(IDomainEvent domainEvent, Func<object, Task> eventPublisher)
    {
        await eventPublisher(domainEvent);
    }
}