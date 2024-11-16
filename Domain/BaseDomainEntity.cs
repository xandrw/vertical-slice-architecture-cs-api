namespace Domain;

public interface IEntity;

public class BaseDomainEntity : IEntity
{
    protected BaseDomainEntity() {}

    /** Auto-Generated and assigned by EF Core, using Reflection */
    public int Id { get; protected set; }

    public static async Task PublishEvent(IDomainEvent domainEvent, Func<object, Task> eventPublisher)
    {
        await eventPublisher(domainEvent);
    }
}