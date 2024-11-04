using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.DeleteUser.Command;

public record DeleteUserCommand(int Id) : IRequest;

public class DeleteUserCommandHandler(IDbProxy<User> usersProxy) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await usersProxy.Query()
            .SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundHttpException<User>();
        }

        usersProxy.Remove(user);
        await usersProxy.SaveChangesAsync(cancellationToken);
    }
}