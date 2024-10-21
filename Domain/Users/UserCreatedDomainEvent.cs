namespace Domain.Users;

public class UserCreatedDomainEvent(User user) : IDomainEvent
{
    public User User { get; } = user;
}