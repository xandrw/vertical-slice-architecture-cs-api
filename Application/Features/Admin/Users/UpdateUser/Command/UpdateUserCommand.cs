using Application.Common.Http.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.UpdateUser.Command;

public record UpdateUserCommand(int Id, string Email, string Role) : IRequest<User>;

public class UpdateUserCommandHandler(IDbProxy<User> usersProxy) : IRequestHandler<UpdateUserCommand, User>
{
    public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await usersProxy.Query().SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null) throw new NotFoundHttpException<User>();

        user.ChangeEmail(command.Email)
            .ChangeRole(command.Role);

        try
        {
            await usersProxy.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<User>();
        }

        return user;
    }
}