using Application.Features.Admin.Users.Common.Http.Exceptions;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.UpdateUser;

public class UpdateUserCommandHandler(IDataProxy<User> dataProxy)
    : IRequestHandler<UpdateUserCommand, User>
{
    public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataProxy.Query()
            .SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (command.Email is not null && command.Email != user.Email)
        {
            var emailExists = await dataProxy.Query().AnyAsync(u => u.Email == command.Email, cancellationToken);

            if (emailExists)
            {
                throw new UserExistsException();
            }
            
            user.ChangeEmail(command.Email);
        }

        if (command.Role is not null)
        {
            user.ChangeRole(command.Role);
        }

        await dataProxy.SaveChangesAsync(cancellationToken);

        return user;
    }
}