using Application.Common.Http.Exceptions;
using Application.Common.Notification;
using Application.Extensions;
using Application.Features.Auth;
using Application.Interfaces;
using Domain.Users;
using Domain.Users.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.CreateUser.Command;

public record CreateUserCommand(string Email, string Role, string Password) : IRequest<User>
{
    public static CreateUserCommand CreateFrom(CreateUserRequest request)
    {
        return new CreateUserCommand(request.Email, request.Role, request.Password);
    }
}

public class CreateUserCommandHandler(
    IDbProxy<User> usersProxy,
    IPasswordHasher passwordHasher,
    EventPublisher eventPublisher) : IRequestHandler<CreateUserCommand, User>
{
    public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.Email, command.Role, command.Password, passwordHasher.HashPassword);

        try
        {
            usersProxy.Add(user);
            await usersProxy.SaveChangesAsync(cancellationToken);
            await eventPublisher.PublishDomainEvent(new UserCreatedDomainEvent(user), cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<User>();
        }

        return user;
    }
}