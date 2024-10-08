using Application.Common.Http.Exceptions;
using Application.Extensions;
using Application.Features.Admin.Users.CreateUser.Http;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.CreateUser;

public class CreateUserCommandHandler(IDataProxy<User> dataProxy, IPasswordHasher passwordHasher)
    : IRequestHandler<CreateUserRequest, User>
{
    public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.Role, request.Password, passwordHasher.HashPassword);

        try
        {
            dataProxy.Add(user);
            await dataProxy.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<User>();
        }

        return user;
    }
}