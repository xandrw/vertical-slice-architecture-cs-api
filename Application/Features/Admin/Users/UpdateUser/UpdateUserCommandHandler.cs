using Application.Extensions;
using Application.Features.Admin.Users.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.UpdateUser;

public class UpdateUserCommandHandler(IDataProxy<User> dataProxy) : IRequestHandler<UpdateUserCommand, User>
{
    public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataProxy.Query().SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null) throw new UserNotFoundException();

        user.ChangeEmail(command.Email)
            .ChangeRole(command.Role);

        try
        {
            await dataProxy.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new UserExistsException();
        }

        return user;
    }
}