using Application.Features.Admin.Users.Common.Http.Exceptions;
using Application.Features.Admin.Users.CreateUser.Http;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.CreateUser;

public class CreateUserCommandHandler(IDataProxy<User> dataProxy) : IRequestHandler<CreateUserRequest, User>
{
    public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await dataProxy.Query()
            .SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser is not null)
        {
            throw new UserExistsException();
        }

        var user = new User(request.Email, request.Role, [], []);
        user.ApplyPassword(request.Password);


        dataProxy.Add(user);
        await dataProxy.SaveChangesAsync(cancellationToken);

        return user;
    }
}