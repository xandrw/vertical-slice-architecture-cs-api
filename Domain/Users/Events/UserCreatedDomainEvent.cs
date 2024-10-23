namespace Domain.Users.Events;

public class UserCreatedDomainEvent(User user) : IDomainEvent
{
    public User User { get; } = user;
}