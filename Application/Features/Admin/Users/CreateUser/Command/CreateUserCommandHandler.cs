using Application.Common.Http.Exceptions;
using Application.Common.Notification;
using Application.Extensions;
using Application.Interfaces;
using Domain.Users;
using Domain.Users.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.CreateUser.Command;

public class CreateUserCommandHandler(IDataProxy<User> dataProxy, IPasswordHasher passwordHasher, Publisher publisher)
    : IRequestHandler<CreateUserRequest, User>
{
    public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.Role, request.Password, passwordHasher.HashPassword);

        try
        {
            dataProxy.Add(user);
            await dataProxy.SaveChangesAsync(cancellationToken);
            await publisher.PublishDomainEvent(new UserCreatedDomainEvent(user), cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<User>();
        }

        return user;
    }
}