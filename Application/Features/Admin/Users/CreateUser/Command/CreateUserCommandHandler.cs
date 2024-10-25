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

public class CreateUserCommandHandler(
    IDbProxy<User> usersProxy,
    IPasswordHasher passwordHasher,
    EventPublisher eventPublisher) : IRequestHandler<CreateUserRequest, User>
{
    public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.Role, request.Password, passwordHasher.HashPassword);

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