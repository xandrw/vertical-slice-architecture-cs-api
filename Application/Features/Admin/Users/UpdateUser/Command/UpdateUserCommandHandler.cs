using Application.Common.Http.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.UpdateUser.Command;

public class UpdateUserCommandHandler(IDataProxy<User> dataProxy) : IRequestHandler<UpdateUserCommand, User>
{
    public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataProxy.Query().SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null) throw new NotFoundHttpException<User>();

        user.ChangeEmail(command.Email)
            .ChangeRole(command.Role);

        try
        {
            await dataProxy.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<User>();
        }

        return user;
    }
}