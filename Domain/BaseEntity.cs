namespace Domain;

public interface IEntity;

public class BaseEntity : IEntity
{
    protected BaseEntity() {}
    
    public static async Task PublishEvent(IDomainEvent domainEvent, Func<object, Task> eventPublisher)
    {
        await eventPublisher(domainEvent);
    }
}