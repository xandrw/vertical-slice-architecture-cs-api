using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.DeleteUser.Command;

public record DeleteUserCommand(int Id) : IRequest;

public class DeleteUserCommandHandler(IRepository<User> usersreRepository) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await usersreRepository.Query()
            .SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundHttpException<User>();
        }

        usersreRepository.Remove(user);
        await usersreRepository.SaveChangesAsync(cancellationToken);
    }
}