using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.ChangePassword.Command;

public record ChangePasswordCommand(int Id, string Password) : IRequest;

public class ChangePasswordCommandHandler(IRepository<User> usersRepository, IPasswordHasher passwordHasher)
    : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.Query().SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundHttpException<User>();
        }

        user.ChangePassword(command.Password, passwordHasher.HashPassword);

        await usersRepository.SaveChangesAsync(cancellationToken);
    }
}