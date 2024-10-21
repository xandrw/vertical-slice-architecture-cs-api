using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.DeleteUser.Command;

public class DeleteUserCommandHandler(IDataProxy<User> dataProxy) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataProxy.Query()
            .SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundHttpException<User>();
        }
        
        dataProxy.Remove(user);

        await dataProxy.SaveChangesAsync(cancellationToken);
    }
}