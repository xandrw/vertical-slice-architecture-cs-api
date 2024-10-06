using Application.Features.Admin.Users.Common.Http.Exceptions;
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
        var existingUser = await dataProxy.Query()
            .SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser is not null)
        {
            throw new UserExistsException();
        }

        var user = new User(request.Email, request.Role, request.Password, passwordHasher.HashPassword);


        dataProxy.Add(user);
        await dataProxy.SaveChangesAsync(cancellationToken);

        return user;
    }
}