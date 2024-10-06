using Application.Features.Admin.Users.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.ChangePassword;

public class ChangePasswordCommandHandler(IDataProxy<User> dataProxy, IPasswordHasher passwordHasher)
    : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await dataProxy.Query().SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.ApplyPassword(command.Password, passwordHasher.HashPassword);

        await dataProxy.SaveChangesAsync(cancellationToken);
    }
}